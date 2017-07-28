/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/3/11
*| 概　要：　フォロワーの処理
*| スクリプト名：　PlayerChiledMovement.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//---------------------------------------------------------------------------------------------
public class PlayerChiledMovement : MonoBehaviour
{

    public Vector2 playerPosition;
    public string playerTag;

    public int fishBulletNum;
    public GameObject playerBullet;
    public float shotDelay;

    private bool shootbulletStop = true;
    private int count = 0;

    private GameObject followerBulletParent;


    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    void Start()
    {
        followerBulletParent = GameObject.Find("FollowerBulletBox");
    }
    
    /*---------------------------------------------------------------------------------------------
    *| 概　要：　マウス移動処理
    *--------------------------------------------------------------------------------------------*/
    void OnMouseDrag()
    {
        // マウスでフォロワーを動かせる
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
        playerPosition = transform.position;
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　当たり判定
    *| 引　数：　コライダー
    *--------------------------------------------------------------------------------------------*/
    void OnTriggerEnter2D(Collider2D c)
    {
        // プレイヤーと接触した時
        if (c.tag == playerTag)
        {
            // 弾を撃つ
            StartCoroutine(Shoot());
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　当たり判定から離れた時
    *| 引　数：　コライダー
    *--------------------------------------------------------------------------------------------*/
    void OnTriggerExit2D(Collider2D c)
    {
        // プレイヤーの接触をやめたとき
        if (c.tag == playerTag)
        {
            // 弾の発射をやめる
            shootbulletStop = true;
            count = 0;
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　弾を撃つ処理
    *--------------------------------------------------------------------------------------------*/
    IEnumerator Shoot()
    {
        shootbulletStop = false;
        for (count = 0; count < fishBulletNum; count++)
        {
            // 弾をプレイヤーと同じ位置/角度で作成
            GameObject newBullet = Instantiate(playerBullet, transform.position, transform.rotation);
            // Hierarchy上でまとめる
            newBullet.transform.SetParent(followerBulletParent.transform);
            // Delay
            yield return new WaitForSeconds(shotDelay);
        }

        if (shootbulletStop == true)
        {
            yield break;
        }
    }
}

//---------------------------------------------------------------------------------------------