using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class Player : MonoBehaviour {

    //イベント設定
    [Serializable] public class EventInt : UnityEvent<HeadDataSt> { }
    [SerializeField] EventInt OnEventInt;
    //宣言
    float Pos;           //現在位置(Y座標)
    float latestPos;     //前の位置(Y座標)
    float HeadSpeed;     //速度
    Vector3 HeadPos;
    GameObject player;   //オブジェクト格納
    Rigidbody rb;        //Rigidbody格納
   
    void Start()
    {
        //Player取得
        player = GameObject.Find("Camera (eye)");
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        //速度初期化
        HeadSpeed = 0.0f;
    }

    void Update()
    {
        //Head関連
        HeadPos = player.transform.position;
        Pos = player.transform.position.y; //現在の位置
        HeadSpeed = ((Pos - latestPos) / Time.deltaTime); //時間あたりの移動距離から速度計算 
        latestPos = player.transform.position.y; //位置更新

        //クラスに放り込む
        HeadDataSt headDataSt;
        headDataSt = new HeadDataSt();

        headDataSt.HeadP = HeadPos;

        //イベント発行
        //if ("ここをどうするか")
        //{
        //    OnEventInt.Invoke(headDataSt); //イベント発行
        //}
    }
}

//データ格納用クラス
public class HeadDataSt
{
    public Vector3 HeadP;
}
