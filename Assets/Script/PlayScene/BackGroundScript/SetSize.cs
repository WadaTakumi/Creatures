/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/6
*| 概　要：　画面比率を調整する
*| スクリプト名：　SetSize.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class SetSize : MonoBehaviour
{
    [SerializeField]
    private Camera Mycamera;
    // 画面のサイズ
    private float width = 640f;
    private float height = 960f;

    // 画像のPixel Per Unit
    private float pixelPerUnit = 100f;


    /*---------------------------------------------------------------------------------------------
    *| 概　要：　更新処理
    *--------------------------------------------------------------------------------------------*/
    void Update()
    {
        float aspect = (float)Screen.height / (float)Screen.width;
        float bgAcpect = height / width;

        Mycamera.orthographicSize = height / 2f / pixelPerUnit;

        if (bgAcpect > aspect)
        {
            // 倍率
            float bgScale = height / Screen.height;
            float camWidth = width / (Screen.width * bgScale);
            Mycamera.rect = new Rect((1f - camWidth) / 2f, 0f, camWidth, 1f);
        }
        else
        {
            float bgScale = width / Screen.width;
            float camHeight = height / (Screen.height * bgScale);
            Mycamera.rect = new Rect(0f, (1f - camHeight) / 2f, 1f, camHeight);
        }
    }
}

//---------------------------------------------------------------------------------------------