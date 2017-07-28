/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/15
*| 概　要：　敵の理処
*| スクリプト名：　EnemyMovement.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class EnemyMovement : MonoBehaviour {
    
    // 移動速度
    public int moveSpeed;
    // 回転速度
    public int rotateSpeed;
    // 生成してからの生存時間（数秒後消すため）
    public float lifeTime;
    // 弾を撃つ間隔
    public float shotDelay;
    // 弾のPrefab
    public GameObject bullet;
    // 体力
    public int health;
    // 発射までの時間
    private float shotDelayTime = 0.0f;
    // ダメージを受けると回りだす
    private bool rotateFlag = false;
    // 死んだときの音
    private AudioSource deathSound;
    // 角度
    private const float angle = 45.0f;

    private GameObject enemyBulletParent;

    /*---------------------------------------------------------------------------------------------
   *| 概　要：　初期化処理
   *--------------------------------------------------------------------------------------------*/
    IEnumerator Start()
    {
        // move to down
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * -moveSpeed;
        // lifeTime秒後に削除
        Destroy(gameObject, lifeTime);

        while (true)
        {
            enemyBulletParent = GameObject.Find("EnemyBulletBox");

            // 弾を撃つ
            Shot(transform);

            // shotDelay秒待つ
            yield return new WaitForSeconds(shotDelay);
        }
    }

    /*---------------------------------------------------------------------------------------------
   *| 概　要：　更新処理
   *--------------------------------------------------------------------------------------------*/
    void Update()
    {
        if (rotateFlag)
        {
            // 回転させる
            transform.Rotate(new Vector3(
                Random.Range(0, angle),
                Random.Range(0, angle),
                Random.Range(0, angle)) * Time.deltaTime * rotateSpeed);
        }
    }

    /*---------------------------------------------------------------------------------------------
   *| 概　要：　更新処理
   *| 引　数：　Transform
   *--------------------------------------------------------------------------------------------*/
    public void Shot(UnityEngine.Transform origin)
    {
        // 親元に生成
        GameObject newBullet = Instantiate(bullet, origin.position, origin.rotation);
        newBullet.transform.SetParent(enemyBulletParent.transform);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　ダメージ計算
    *| 引　数：　ダメージ量
    *--------------------------------------------------------------------------------------------*/
    public void TakeDamage(int amount)
    {
        health -= amount;
        // 敵を回す
        rotateFlag = true;
        // もし体力が０になったら破壊
        if (health <= 0)
        {
            Destroy(gameObject);
            //deathSound.PlayOneShot(deathSound.clip);
        }
    }
}
