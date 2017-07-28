/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/3/8
*| 概　要：　プレイヤーの追跡（フォロワー）
*| スクリプト名：　flock.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class flock : MonoBehaviour
{
   
    public float speed = 0.001f;
    float rotationSpeed = 4.0f;
    // 平均進行方向
    Vector3 avarageHeading; 
    Vector3 avaragePosition;
    // 隣接距離
    float neighbourDistance = 3.0f; 

    bool turning = false;
    // predatorの追うべきポジション
    Vector3 goalPos;


    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    void Start()
    {
        // プレデター各々のスピードをランダムに初期化
        speed = Random.Range(0.5f, 1);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　更新処理
    *--------------------------------------------------------------------------------------------*/
    void Update()
    {
        // ターゲットを取得
        goalPos = globalFlock.goalPos;

        // 画面外に（タンク内から）出そうなら引き返すためのフラグを立てる
        // 距離を求める
        if (Vector3.Distance(transform.position, Vector3.zero) >= globalFlock.tankSize)
            turning = true;
        
        //タンクの範囲外にいないのであれば好転する必要はない
        else turning = false;

        // 画面外に（タンクの中から）出そうなら
        if (turning)
        {
            // 向きを変更
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    rotationSpeed * Time.deltaTime);
            // ターン速度もまたランダムにする  
            speed = Random.Range(0.5f, 1);          
        }

        // 画面内（タンクの中）なら
        else if (Random.Range(0, 5) < 1)
            // ルールに従って行動する
            ApplyRules();

        // プレデターを移動させる
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　Flockingのルールに従う
    *---------------------------------------------------------------------------------------------*/
    void ApplyRules()
    {
        GameObject[] gos;
        gos = globalFlock.allFish;

        Vector3 vcentre = Vector3.zero; // グループの中心に向かって指す
        Vector3 vavoid = Vector3.zero;  // 隣人から離れる(避ける)
        float gSpeed = 0.1f;    // グループ全体のスピード

        // 追跡目標
        goalPos = globalFlock.goalPos;
        // 距離
        float dist; 
        // 初期化
        int groupSize = 0;

        // グループがない場合
        // グループを作る
        foreach (GameObject go in gos)
        {
            if (go == null)
                return;

            if (go != this.gameObject)
            {
                // 距離を
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                
                // もし近づきすぎたら
                if (dist <= neighbourDistance)  
                {
                    // 中心
                    vcentre+= go.transform.position;
                    // グループサイズを広げる
                    groupSize++;
                    
                    // 回避行動
                    if (dist < 1.0f)
                        // プレデター同士が重ならないようにする為に互いが避ける
                        vavoid = vavoid + (this.transform.position - go.transform.position);

                    // 平均速度を取得
                    flock anotherFlock = go.GetComponent<flock>();
                    // グループとしての速度にする
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        // グループがある場合
        if (groupSize > 0)
        {
            // 平均中心と平均速度
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            // ターゲットに向かう
            Vector3 direction = (vcentre + vavoid) - transform.position;
            
            if (direction != Vector3.zero)
            {
                // ターゲットの方向に向く
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
            }
        }
    }

}

//---------------------------------------------------------------------------------------------