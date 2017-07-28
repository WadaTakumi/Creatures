/*_____________________________________________________________________
*|
*| 制作者：　和田拓実
*| 概　要：　A*
*| スクリプト名：　AStarMovement.cs
*|
*| Copylight ©2017 TAKUMI WADA ALL Right Reserved
*|____________________________________________________________________*/
//---------------------------------------------------------------------------------------------
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//---------------------------------------------------------------------------------------------
//public struct Node
//{
//    public Vector2 goal;          // goal pos
//    public Vector2 start;         // start pos 

//    public bool empty;

//    // 移動コスト
//    double moveCost;
//    // 推定コスト
//    double heuristicCost;


//    public Node (Vector2 position) : this()
//    {
//        goal = position;
//        empty = true;

//        heuristicCost = Math.Sqrt(
//            Math.Pow(AStarMovement.goMathalPosition.x - goal.x, 2) +
//            Math.Pow(AStarMovement.goalPosition.y - goal.y, 2));
//    }

//    double GetScore()
//    {
//        return moveCost + heuristicCost;
//    }

//}

//public class AStarMovement : MonoBehaviour {

//    ///InfluenceMap influenceMap;
//    public static Vector2 goalPosition;
//    public static Vector2 goMathalPosition;

//    int[,] influenceMap;

//    public Node[,] nodes;
//    public Node[,] opennodes;
//    public Node[,] closednodes;

//    // インフルエンスマップを受け取る
//    void GetinfluenceMap(int[,] map)
//    {
//        influenceMap = map;
//    }

//    public void Initialized(int size)
//    {
//        goalPosition = new Vector2(fieldSize - 1, fieldSize - 1);
//    }


//    public void Reset()
//    {
//        GameObject node;

//        // リストを初期化
//        for (int x = 0; x < fieldSize; x++)
//        {
//            for (int y = 0; y < fieldSize; y++)
//            {
//                node = GameObject.Find(String.Format("AStar/Node_{0, 2:00}_{1, 2:00}", x, y));
//                node.GetComponent<MeshRenderer>().material = normalMat;
//                nodes[x, y] = new Node(new Vector2(x, y));
//                opennodes[x, y] = new Node(new Vector2(x, y));
//                closednodes[x, y] = new Node(new Vector2(x, y));
//            }
//        }

//        foreach (Transform child in transform)
//        {
//            if (child.name == "Point(Clone)" || child.name == "Beam(Clone)")
//            {
//                Destroy(child.gameObject);
//            }
//        }

//        // スタートとゴールの設置
//        GetComponent<Field>().SetPoint();
//    }



//    IEnumerator PassFinding()
//    {

//        while (true)
//        {
//            // いつまで探索する必要があるのか
//            // if(){break;}
//        }

//        // 0.5秒毎に呼び出す
//        //yield return new WaitForSeconds(0.5f);
//        // 一フレーム待つ（必要）
//        //yield return null;
//    }


//    IEnumerator OpenNode()
//    {
//        for(int hx = -1;hx < 2; hx++)
//        {
//            for(int hy = -1; hy <2; hy++)
//            {
//                // 開始地点は必ず移動コスト（開始点からの移動量）が０
//                if (hx == 0 && hy == 0)
//                {
//                    continue;
//                }
//            }
//        }


//        yield return new WaitForSeconds(0.01f);
//    }

//    IEnumerator CloseNode()
//    {
//        yield return new WaitForSeconds(0.01f);
//    }


//}

//---------------------------------------------------------------------------------------------