/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 作成日：　2017/2/23
*| 概　要：　エミッター
*| スクリプト名：　Emitter.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class Emitter : MonoBehaviour
{
    // 何秒おきに敵を生成するか
    public int createWaveTime; 
    // Waveプレハブを格納する
    public GameObject[] waves;
    // 現在のWave
    private int currentWave;
    // 時間
    private float time = 0.0f;
    // ゲーム進行中かどうか
    private bool isPlay = true;

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　更新処理
    *--------------------------------------------------------------------------------------------*/
    void Update()
    {
        Emit(true);
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　セット
    *| 引　数：　プレイ中かどうかのフラグ
    *--------------------------------------------------------------------------------------------*/
    public void SetIsPlay(bool flag)
    {
        isPlay = flag;
    }

    /*---------------------------------------------------------------------------------------------
    *| 概　要：　waveの放出処理
    *| 引　数：　プレイ中かどうかのフラグ
    *--------------------------------------------------------------------------------------------*/
    public void Emit(bool flag)
    {
        //if (!flag) Debug.Break();

        if (isPlay)
        {
            // 時間を更新
            time += Time.deltaTime;

            // create enemy
            if (time >= createWaveTime && isPlay)
            {
                // 時間を初期化
                time = 0.0f;

                // waveから敵を生成（向きも初期化して出す）
                if (currentWave != waves.Length)
                {
                    GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);
                    // 親にする
                    wave.transform.parent = transform;
                    // 向きを変える
                    wave.transform.localRotation = waves[currentWave].transform.localRotation;
                    // waveを更新
                    ++currentWave;
                }
            }
        }
    }
}

//---------------------------------------------------------------------------------------------