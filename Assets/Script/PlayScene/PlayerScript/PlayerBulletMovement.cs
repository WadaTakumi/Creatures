/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/9
*| 概　要：　プレイヤーの撃つ弾の処理
*| スクリプト名：　PlayerBulletManager.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class PlayerBulletMovement : MonoBehaviour {

    public int speed;
    public float lifeTime;

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    void Start()
    {
        // move bullet(down direction)
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
        // lifeTime秒後に削除
        Destroy(gameObject, lifeTime);
    }
}

//---------------------------------------------------------------------------------------------