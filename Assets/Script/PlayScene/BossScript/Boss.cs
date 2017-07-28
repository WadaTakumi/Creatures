/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/17
*| 概　要：　ボスの行動
*| スクリプト名：　Boss.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
// Rigidbody2Dコンポーネントを必須にする
//[RequireComponent(typeof(Rigidbody2D))]

//---------------------------------------------------------------------------------------------
public class Boss : MonoBehaviour {

    // 移動速度
    public float moveSpeed;
    // 回転速度
    public float rotateSpeed;
    // 弾を撃つ間隔
    public float shotDelay;
    // 弾に関する時間
    private float shotTime = 0.0f;
    // 弾のPrefab
    public GameObject bullet; 
    // 弾を撃つかどうか
    public bool canShot;
    // 移動に関する時間
    private float moveTime = 0.0f;
    // 方向
    private Vector2 dir;    
    // 出現時
    private bool first = true;
    // 合計体力
    public int health;  
    // ボスのパターンが変わる体力の値
    public int changePatterunHelthNum1;
    public int changePatterunHelthNum2;
    //public int changePatterunHelthNum3;
    // マネージャー
    Manager manager;

    // Wave
    private Transform wave;

    // エミッター
    [SerializeField]
    Emitter emitter;

    private float rotateTime = 0.0f;

    // 移動関係のパラメータ
    public float movementPattern1MoveFirstTime;
    public float movementPattern2MoveFirstTime;
    public float movementPattern3MoveFirstTime;
    public float movementPattern1NextTime;
    public float movementPattern2NextTime;
    public float movementPattern1MoveSpeed;
    public float movementPattern2MoveSpeed;
    public float movementPattern3RotateSpeed_X;
    public float movementPattern3RotateSpeed_Y;

    // 攻撃関係のパラメータ
    public float attackPattern2RotateFirstTime;
    public float attackPattern2RotateNextTime;
    public float attackPattern2RotateSpeed;


    // 移動方向
    private enum MoveDirection
    {
        MOVE_UNKWOWN,
        MOVE_CENTER,
        MOVE_LEFT,
        MOVE_RIGHT,
        MOVE_ROTATE
    }
    // 移動させる向き
    MoveDirection moveDirection;
    // 現在の向き
    MoveDirection cullentMoveDirection; 

    // 行動パターン
    private enum MovePattern
    {
        MOVE_PATTERN1,
        MOVE_PATTERN2,
        MOVE_PATTERN3
    }
    MovePattern movePattern;

    // 攻撃パターン
    private enum AttackPattern
    {
        ATTACK_PATTERN1,
        ATTACK_PATTERN2
    }
    AttackPattern attackPattern;

    // 回転方向
    private enum RotateDirection
    {
        ROTATE_CENTER,
        ROTATE_LEFT,
        ROTATE_RIGHT
    }
    // 向かせる向き
    RotateDirection rotateDirection;
    // 現在の向き
    RotateDirection culletntRotateDirection;

    private GameObject bulletParent;

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    void Start()
    {
        // 動的に取る
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        wave = transform.parent.gameObject.transform;
        movePattern = MovePattern.MOVE_PATTERN1;
        attackPattern = AttackPattern.ATTACK_PATTERN1;
        cullentMoveDirection = MoveDirection.MOVE_CENTER;

        // ローカル座標のY軸のマイナス方向に移動する
        // Movement
        Move(transform.right * -1);
        bulletParent = GameObject.Find("EnemyBulletBox");
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　更新処理
    *--------------------------------------------------------------------------------------------*/
    void Update()
    {
        // 弾を打つ
        shotTime += Time.deltaTime;
        if (shotTime >= shotDelay)
        { 
            shotTime = 0.0f;
            // shotPositionの位置、角度で弾を撃つ
            Shot(transform);
        }

        // 移動の制御
        Movement();
        // 向きの制御
        Rotate();
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　動き
    *--------------------------------------------------------------------------------------------*/
    private void Movement()
    {
        // 最初にとる行動を初期化
        // (一度通ったら通らない)
        if (first)
        {
            first = false;
            moveDirection = MoveDirection.MOVE_CENTER;
            rotateDirection = RotateDirection.ROTATE_CENTER;
        }

        if (!first)
        {
            moveTime += Time.deltaTime;
            
            // 行動パターン
            switch (movePattern)
            {
                case MovePattern.MOVE_PATTERN1:

                    // 攻撃パターンを１にする
                    attackPattern = AttackPattern.ATTACK_PATTERN1;

                    // 移動方向
                    switch (moveDirection)
                    {
                        case MoveDirection.MOVE_CENTER:
                            MoveCenter(movementPattern1MoveFirstTime, movementPattern1NextTime, movementPattern1MoveSpeed);
                            break;
                        case MoveDirection.MOVE_LEFT:
                            MoveLeft(movementPattern1NextTime, movementPattern1MoveSpeed);
                            break;
                        case MoveDirection.MOVE_RIGHT:
                            MoveRight(movementPattern1NextTime, movementPattern1MoveSpeed);
                            break;
                    }

                    // もし体力が一定値を下回ったら行動パターン2に切り替える
                    ChangeMovePattern(MovePattern.MOVE_PATTERN2);

                    break;

                case MovePattern.MOVE_PATTERN2:

                    // 攻撃パターンを２にする
                    attackPattern = AttackPattern.ATTACK_PATTERN2;
                    
                    // 移動方向
                    switch (moveDirection)
                    {
                        case MoveDirection.MOVE_CENTER:
                            MoveCenter(movementPattern2MoveFirstTime, movementPattern2NextTime, movementPattern2MoveSpeed);
                            break;
                        case MoveDirection.MOVE_LEFT:
                            MoveLeft(movementPattern2NextTime, movementPattern2MoveSpeed);
                            break;
                        case MoveDirection.MOVE_RIGHT:
                            MoveRight(movementPattern2NextTime, movementPattern2MoveSpeed);
                            break;
                    }
                    
                    // もし体力が一定値を下回ったら行動パターン2に切り替える
                    ChangeMovePattern(MovePattern.MOVE_PATTERN3);

                    break;

                case MovePattern.MOVE_PATTERN3:
                    
                    // 移動方向
                    switch (moveDirection)
                    {
                        case MoveDirection.MOVE_CENTER:
                    
                            // 真ん中に移動
                            Vector2 targetPos = new Vector2(0.0f, 0.0f);
                            transform.position = Vector2.MoveTowards(transform.position, targetPos,  Time.deltaTime);
                    
                            if (moveTime >= movementPattern3MoveFirstTime)
                            {
                                moveTime = 0.0f;
                                moveDirection = MoveDirection.MOVE_ROTATE;
                            }
                            break;
                    
                        case MoveDirection.MOVE_ROTATE:
                            MoveRotatte();
                            break;
                    }
                    break;
            }
        }
    }


    /*---------------------------------------------------------------------------------------------
    *| 概　要：　真ん中に移動
    *| 引　数：　最初に移動する際のDerayタイム、
    *                   次の方向に移動する際のDerayタイム,、
    *                   移動スピード
    *--------------------------------------------------------------------------------------------*/
    private void MoveCenter(float firstTime, float nextTime, float speed)
    {
        // 真ん中に移動
        Vector2 targetPos = new Vector2(0.0f, 1.5f);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);


        // 最初の移動方向は左
        if (moveTime >= firstTime &&
            cullentMoveDirection == MoveDirection.MOVE_CENTER)
        {
            moveTime = 0.0f;
            moveDirection = MoveDirection.MOVE_LEFT;
        }



        // 前回の移動方向がそのまま左なら左に移動
        if (cullentMoveDirection == MoveDirection.MOVE_LEFT)
        {
            //GetComponent<Rigidbody2D>().velocity = dir * moveSpeed;
            Vector2 target = new Vector2(5, 1.5f);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // 時間が経つと右移動に変更
            if (moveTime >= nextTime)
            {
                moveTime = 0.0f;
                moveDirection = MoveDirection.MOVE_RIGHT;
            }
        }

        // 前回の移動方向が右ならそのまま右に移動
        if (cullentMoveDirection == MoveDirection.MOVE_RIGHT)
        {
            //GetComponent<Rigidbody2D>().velocity = dir * -moveSpeed;
            Vector2 target = new Vector2(-5, 1.5f);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // 時間が経つと左移動に変更
            if (moveTime >= nextTime)
            {
                moveTime = 0.0f;
                moveDirection = MoveDirection.MOVE_LEFT;
            }
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　左に移動
    *| 引　数：　次の行動を開始する際の時間、
    *                   移動スピード
    *--------------------------------------------------------------------------------------------*/
    private void MoveLeft(float time, float speed)
    {
        cullentMoveDirection = MoveDirection.MOVE_LEFT;
        //GetComponent<Rigidbody2D>().velocity = dir * -moveSpeed;
        Vector2 targetPos = new Vector2(-5, 1.5f);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // 時間が経つと真ん中に移動
        if (moveTime >= time)
        {
            moveTime = 0.0f;
            moveDirection = MoveDirection.MOVE_CENTER;
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　右に移動
    *| 引　数：　次の行動を開始する際の時間、
    *                   移動スピード
    *--------------------------------------------------------------------------------------------*/
    private void MoveRight(float time, float speed)
    {
        cullentMoveDirection = MoveDirection.MOVE_RIGHT;
        //GetComponent<Rigidbody2D>().velocity = dir * moveSpeed;
        Vector2 targetPos = new Vector2(5, 1.5f);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        // 時間が経つと真ん中に移動
        if (moveTime >= time)
        {
            moveTime = 0.0f;
            moveDirection = MoveDirection.MOVE_CENTER;
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　円状に移動
    *--------------------------------------------------------------------------------------------*/
    private void MoveRotatte()
    {
        float x = 4.0f * Mathf.Sin(Time.time * 2);
        float y = 2.0f * Mathf.Cos(Time.time * 2);
        transform.position = new Vector2(x, y - 0.5f);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　向きを変える
    *--------------------------------------------------------------------------------------------*/
    private void Rotate()
    {
        rotateTime += Time.deltaTime;

        switch (attackPattern)
        {
            case AttackPattern.ATTACK_PATTERN1:
                break;
            case AttackPattern.ATTACK_PATTERN2:
                switch (rotateDirection)
                {
                    case RotateDirection.ROTATE_CENTER:
                        RotateCenter(attackPattern2RotateFirstTime, attackPattern2RotateNextTime, attackPattern2RotateSpeed);
                        break;

                    case RotateDirection.ROTATE_LEFT:
                        RotateLeft(attackPattern2RotateNextTime, attackPattern2RotateSpeed);
                        break;

                    case RotateDirection.ROTATE_RIGHT:
                        RotateRight(attackPattern2RotateNextTime, attackPattern2RotateSpeed);
                        break;
                }
                break;
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　真ん中に向けて回転
    *| 引　数：　最初に移動する際のDerayタイム、
    *                   次の方向に移動する際のDerayタイム,、
    *                   移動スピード
    *--------------------------------------------------------------------------------------------*/
    private void RotateCenter(float firstTime, float nextTime, float speed)
    {
        // 最初は左に移動
        if (rotateTime >= firstTime &&
            culletntRotateDirection == RotateDirection.ROTATE_CENTER)
        {
            rotateTime = 0.0f;
            rotateDirection = RotateDirection.ROTATE_LEFT;
        }

        // 前回の移動方向が左ならそのまま左に移動
        if (culletntRotateDirection == RotateDirection.ROTATE_LEFT)
        {
            transform.Rotate(new Vector3(0, 5, 0) * Time.deltaTime * speed);
            if (rotateTime >= nextTime)
            {
                rotateTime = 0.0f;
                rotateDirection = RotateDirection.ROTATE_RIGHT;
            }
        }

        // 前回の移動方向が右ならそのまま右に移動
        if (culletntRotateDirection == RotateDirection.ROTATE_RIGHT)
        {
            transform.Rotate(new Vector3(0, -5, 0) * Time.deltaTime * speed);
            if (rotateTime >= nextTime)
            {
                rotateTime = 0.0f;
                rotateDirection = RotateDirection.ROTATE_LEFT;
            }
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　左向きに回転
    *| 引　数：　次の行動を開始する際の時間、
    *                   移動スピード
    *--------------------------------------------------------------------------------------------*/
    private void RotateLeft(float time, float speed)
    {
        culletntRotateDirection = RotateDirection.ROTATE_LEFT;
        transform.Rotate(new Vector3(0, -5, 0) * Time.deltaTime * speed);
        // 真ん中の向きに戻る
        if (rotateTime >= time)
        {
            rotateTime = 0.0f;
            rotateDirection = RotateDirection.ROTATE_CENTER;
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　右向きに回転
    *| 引　数：　次の行動を開始する際の時間、
    *                   移動スピード
    *--------------------------------------------------------------------------------------------*/
    private void RotateRight(float time, float speed)
    {
        culletntRotateDirection = RotateDirection.ROTATE_RIGHT;
        transform.Rotate(new Vector3(0, 5, 0) * Time.deltaTime * speed);
        // 真ん中の向きに戻る
        if (rotateTime >= time)
        {
            rotateTime = 0.0f;
            rotateDirection = RotateDirection.ROTATE_CENTER;
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　行動パターンを変える
    *| 引　数：　行動パターン
    *--------------------------------------------------------------------------------------------*/
    void ChangeMovePattern(MovePattern pattern)
    {
        // もし体力が一定値を下回ったら次の行動パターンに切り替える
        // 行動パターン2にする
        if (health <= changePatterunHelthNum1 
            && health > changePatterunHelthNum2 
            && movePattern == MovePattern.MOVE_PATTERN1)
        {
            moveTime = 0.0f;
            movePattern = pattern;
            cullentMoveDirection = MoveDirection.MOVE_CENTER;
            moveDirection = MoveDirection.MOVE_CENTER;
        }

        // 行動パターン3にする
        if (health <= changePatterunHelthNum2 &&
            health > 0 &&
            movePattern == MovePattern.MOVE_PATTERN2)
        {
            //moveTime = 0.0f;
            movePattern = pattern;
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　敵の移動
    *| 引　数：　Transform
    *--------------------------------------------------------------------------------------------*/
    public void Shot(UnityEngine.Transform origin)
    {
        // 親元に生成
        GameObject newBullets = Instantiate(bullet, origin.position, origin.rotation);
        // ボスの位置を考慮した、新しい場所を設定する
        newBullets.transform.position = origin.position;
        //newBullets.transform.localPosition -= new Vector3(0, -5, 0);
        // Hierarchy上でまとめる
        newBullets.transform.SetParent(bulletParent.transform);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　敵の移動
    *| 引　数：　方向
    *--------------------------------------------------------------------------------------------*/
    public void Move(UnityEngine.Vector2 direction)
    {
        dir = direction;
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　ダメージ計算
    *| 引　数：　ダメージ量
    *--------------------------------------------------------------------------------------------*/
    public void TakeDamage(int amount)
    {
        health -= amount;

        // 体力が０になると破壊
        if (health <= 0)
        {
            // ゲームクリアを知らせる
            //manager.SendMessage("desisionOver", "clear", SendMessageOptions.DontRequireReceiver);
            manager.DesisionOver(true);
            Destroy(gameObject);
        }
    }

    // 
    //public void SetGameManager(Manager man)
    //{
    //    this.manager = man;
    //}
    /*---------------------------------------------------------------------------------------------
    *| 概　要：　エミッターをセット
    *| 引　数：　エミッター
    *--------------------------------------------------------------------------------------------*/
    public void SetEmitter(Emitter emi)
    {
        this.emitter = emi;
    }
}

//---------------------------------------------------------------------------------------------