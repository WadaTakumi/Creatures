/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/9
*| 概　要：　プレイヤーの処理
*| スクリプト名：　PlayerMovement.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class PlayerMovement : MonoBehaviour
{
    public Manager manager;
    public Vector2 playerPosition;
    // 体力
    public float health;
    private AudioSource deathSound;
    GameObject player;

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        deathSound = GetComponent<AudioSource>();
        //player = GameObject.Find("Player");
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　マウス移動処理
    *--------------------------------------------------------------------------------------------*/
    void OnMouseDrag()
    {
        // Player come with mouse position
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;

        // get to Player position
        playerPosition = transform.position;
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　ダメージ計算
    *| 引　数：　ダメージ量
    *--------------------------------------------------------------------------------------------*/
    public void TakeDamage(int amount)
    {
        health -= amount;

        // もし体力が０になったら破壊
        if (health <= 0)
        {
            // ゲームオーバーを知らせる
            //manager.SendMessage("desisionOver", false, SendMessageOptions.DontRequireReceiver);
            manager.DesisionOver(false);

            // 破壊
            Destroy(gameObject);
            // 音を鳴らす
            deathSound.PlayOneShot(deathSound.clip);
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　当たり判定
    *| 引　数：　コライダー
    *--------------------------------------------------------------------------------------------*/
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            // ゲームオーバーを知らせる
            //manager.SendMessage("desisionOver", true);
            manager.DesisionOver(false);
            // 破壊
            Destroy(gameObject);
            // 音を鳴らす
            deathSound.PlayOneShot(deathSound.clip);
        }
    }
}

//---------------------------------------------------------------------------------------------