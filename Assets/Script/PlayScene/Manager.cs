/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/24
*| 概　要：　ゲームマネージャー
*| スクリプト名：　Manager.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//---------------------------------------------------------------------------------------------
public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject clearText;

    [SerializeField]
    private GameObject gameOverText;

    [SerializeField]
    private GameObject backTitleSceneButon;

    [SerializeField]
    private Emitter emiiter;

    [SerializeField]
    private globalFlock globalFlock;

    // 終了判定
    private bool overed = false;

    private float time = 0.0f;

    //InfluenceMap influenceMap;
    //int influCount = 0;

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    void Start()
    {
        //influenceMap = new InfluenceMap(6.4f, 10.0f, 0.6f);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　更新処理
    *--------------------------------------------------------------------------------------------*/
    void Update()
    {
        // 終了判定後ボタンを表示する
        if (overed)
        {
            time += Time.deltaTime;
            if (time >= 3)
            {
                backTitleSceneButon.SetActive(true);
            }
        }

        //if(influCount >= 5)
        //{
        //    influenceMap.MapClear();
        //    influenceMap.SetVaule(3.3f, 5.5f, 10, 1);
        //
        //    influCount = 0;
        //}
        //influCount++;
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　シーンを変える
    *| 引　数：　シーン番号
    *--------------------------------------------------------------------------------------------*/
    public void SceneChanger(int num)
    {
        SceneManager.LoadScene(num);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　終了結果を決め
    *| 引　数：　ゲームクリア
    *--------------------------------------------------------------------------------------------*/
    public void DesisionOver(bool clear)
    {
        overed = true;
        emiiter.SetIsPlay(false);

        if (clear == true)
            clearText.SetActive(true);
        
        if (clear == false)
            gameOverText.SetActive(true);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　セット
    *| 引　数：　グローバルフロック
    *--------------------------------------------------------------------------------------------*/
    public void SetGlobalFlock(globalFlock glFlock)
    {
        globalFlock = glFlock;
    }


    //private bool flag;
    //public bool Flag
    //{
    //    get
    //    {
    //        return flag;
    //    }
    //    set
    //    {
    //        flag = value;
    //    }
    //}

}

//---------------------------------------------------------------------------------------------