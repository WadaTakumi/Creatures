/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 概　要：　インフルエンスマップ
*| スクリプト名：　InfluenceMap.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------------------
public class InfluenceMap {

    int[,] influenceMap;
    int[,] influenceMap2;
    float defaultSpeed = 0.0f;
    int mapSizeX, mapSizeY;

    // 画面のサイズと、１フレームあたりのスピード
    public InfluenceMap(float sizeX, float sizeY, float speed)
    {
        // インフルエンスマップの初期化
        influenceMap = new int[(int)(sizeX / speed + 0.5f), (int)(sizeY / speed + 0.5f)];

        mapSizeX = (int)(sizeX / speed + 0.5f);
        mapSizeY = (int)(sizeY / speed + 0.5f);
        defaultSpeed = speed;
    }

    // マップをクリア（０にする）
    public void MapClear()
    {
        for(int i =0; i < mapSizeX; i++)
        {
            for(int j=0;j < mapSizeY; j++)
            {
                influenceMap[i, j] = 0;
            }
        }
    }

    // 画面サイズ内の座標と影響力、減衰率をセット
    public void SetVaule(float x, float y, int forceValue, int decay)
    {
        int setX = (int)(x / defaultSpeed);
        int setY = (int)(y / defaultSpeed);

        influenceMap2 = new int[mapSizeX, mapSizeY];

        SetValueForce(setX, setY, forceValue, decay);
    }

    // 再帰呼び出しで影響マップに値を書き込む
    // （自分自身と同じ座標はチェック済み（ロック）にする）
    private void SetValueForce(int x, int y, int forceValue, int decay)
    {
        // forceValueが0以下だったらreturn
        if (forceValue <= 0) return;
        // x, yどちらかが範囲外ならreturn
        if (x < 0 || y < 0 ||
            x >= mapSizeX || y >= mapSizeY)
            return;

        // 自身がロックされていたらreturn
        if (influenceMap2[x, y] == 1)
            return;

        // 自分の座標に影響値を(加算で)入れる
        influenceMap[x, y] += forceValue;

        // map2にロック値を入れる
        influenceMap2[x, y] = 1;

        // 再帰関数として自信を呼び出す
        // 上
        SetValueForce(x, y - 1, forceValue - decay, decay);
        // 右
        SetValueForce(x + 1, y, forceValue - decay, decay);
        // 下
        SetValueForce(x, y + 1, forceValue - decay, decay);
        // 左
        SetValueForce(x - 1, y, forceValue - decay, decay);
    }

}

//---------------------------------------------------------------------------------------------