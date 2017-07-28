/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/11
*| 概　要：　弾の処理
*| スクリプト名：　Bullet.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class Bullet : MonoBehaviour {

    //public string bulletTag;
    public string targetTag;
    private AudioSource collisionSound;

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    void Start()
    {
        collisionSound = GetComponent<AudioSource>();
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　当たり判定
    *| 引　数：　コライダー
    *--------------------------------------------------------------------------------------------*/
    void OnTriggerEnter2D(Collider2D c)
    {
        // bullet and bullet
        //if (c.tag == bulletTag)
        //{
        //    // 弾の削除
        //    // destroy to bullet
        //    //Destroy(c.gameObject);
        //}

        // is player or is enemy or spider
        if (c.tag == targetTag)
        {
            // ダメージを与える
            c.SendMessage("TakeDamage", 1);

            // destroy Bullet
            Destroy(gameObject);
            //collisionSound.PlayOneShot(collisionSound.clip);
        }
    }
}

//---------------------------------------------------------------------------------------------