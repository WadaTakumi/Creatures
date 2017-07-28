/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/10
*| 概　要：　弾を撃つ
*| スクリプト名：　ShotBullets.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class ShotBullets : MonoBehaviour {

    // PlayerBulletプレハブ
    public GameObject playerBullet;

    // 弾を撃つ間隔
    public float shotDelay;

    private GameObject playerBulletparent;
    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    // Startメソッドをコルーチンとして呼び出す
    IEnumerator Start()
   {
        while (true)
       {
            playerBulletparent = GameObject.Find("PlayerBulletBox");
            // 弾をプレイヤーと同じ位置/角度で作成
            GameObject newBullet = Instantiate(playerBullet, transform.position, transform.rotation);
            // Hierarchy上でまとめる
            newBullet.transform.SetParent(playerBulletparent.transform);
            // Delay
            yield return new WaitForSeconds(shotDelay);
       }
   }
}

//---------------------------------------------------------------------------------------------