/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/3/8
*| 概　要：　プレイヤーの追跡（フォロワー）
*| スクリプト名：　globalFlock.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class globalFlock : MonoBehaviour
{
    // プレデター
    public GameObject fishPrefab;
    // ターゲット
    public GameObject goalPrefab;
    public static int tankSize = 5;
    // フォロワーの数
    static int numFish = 6;
    public static GameObject[] allFish = new GameObject[numFish];
    // 到着地点
    public static Vector2 goalPos = Vector2.zero;
    // 追跡するかどうかのフラグ
    private bool chaseFlag = true;

    private GameObject followerParent;

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　初期化処理
    *--------------------------------------------------------------------------------------------*/
    void Start()
    {
        followerParent = GameObject.Find("FollowerBox");

        for (int i = 0; i < numFish; i++)
        {
            // ランダムでプレデターの初期位置を生成
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                                      Random.Range(-5, -7),
                                      Random.Range(-tankSize, tankSize));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            // Hierarchy上でまとめる
            allFish[i].transform.SetParent(followerParent.transform);
        }
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　更新処理
    *--------------------------------------------------------------------------------------------*/
    void Update()
    {
        // プレイヤーが生きてるなら追跡を続ける
        if (null != goalPrefab)  goalPos = goalPrefab.transform.position;
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　追跡フラグを受け取る
    *|引　数：　フラグ 
    *--------------------------------------------------------------------------------------------*/
    public void SetChaseFlag(bool flag)
    {
        chaseFlag = flag;
    }
}

//---------------------------------------------------------------------------------------------