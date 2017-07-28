/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 概　要：　弾の移動処理
*| スクリプト名：　SceneChanger.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//---------------------------------------------------------------------------------------------
public class SceneChanger : MonoBehaviour {

    //public static SceneChanger instance = null;

    //private Scene titleScene;
    //private void Awake()
    //{
    //    if(!instance)
    //    {
    //        instance = this;
    //    }else if(!this)
    //    {
    //        Destroy(gameObject);
    //    }

    //    DontDestroyOnLoad(gameObject);
    //}

    //private void Start()
    //{
    //    titleScene = SceneManager.GetSceneByName("TitleScene");
    //
    //    Debug.Log(titleScene);
    //    Debug.Log(SceneManager.GetActiveScene());
    //}
    //private void OnMouseDown()
    //{
    //    if(SceneManager.GetActiveScene() == titleScene)
    //    {
    //        SceneManager.LoadScene("PlayScene");
    //    }
    //}

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　シーン移動処理
    *--------------------------------------------------------------------------------------------*/
    public void ChangeSceneTo(int num)
    {
        SceneManager.LoadScene(num);
    }
}

//---------------------------------------------------------------------------------------------
