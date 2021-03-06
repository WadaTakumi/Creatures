﻿/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/7
*| 概　要：　背景画像のスクロール
*| スクリプト名：　Scroll.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class Scroll : MonoBehaviour {

    public float speed;
    float endPosition = -9.5f;
    Vector3 startPosition = new Vector3(0f, 9.4f, 15f);


    /*---------------------------------------------------------------------------------------------
    *| 概　要：　更新処理
    *--------------------------------------------------------------------------------------------*/
    void Update ()
    {
        // 画面を下方向に移動
        this.transform.position += new Vector3(0f, -speed);

        // 位置を再設定
        if (this.transform.position.y < endPosition)
            this.transform.position = startPosition;

    }
}

//---------------------------------------------------------------------------------------------