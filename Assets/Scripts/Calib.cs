using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class Calib : MonoBehaviour
{
    // イベント設定
    [Serializable] public class EventInt : UnityEvent<IntialDate> { }
    [SerializeField] EventInt CalibEvent;
    //変数宣言

    // VRデバイス設定
    SteamVR_Controller.Device head = null;

    // データ格納用
    public float headSpeed;     //速度

    // 【新規】データ格納用
    public Vector3 standPos = new Vector3(0.0f, 0.0f, 0.0f); // 立ち状態 -> JumpMin
    public Vector3 crouchPos = new Vector3(0.0f, 0.0f, 0.0f); // しゃがみ状態 -> MinPos
    public Vector3 jumpMaxPos = new Vector3(0.0f, 0.0f, 0.0f); // ジャンプ最大 -> MaxPos

    public GameObject calibText = null; // データ表示用

    // Use this for initialization
    void Start()
    {
        head = SteamVR_Controller.Input((int)SteamVR_TrackedObject.EIndex.Hmd); //HMD取得
        standPos = head.transform.pos; // 立ちの値をHMDの初期値に
        crouchPos = head.transform.pos; //　最上地をHMDの初期値に
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    // Update is called once per frame
    void Update()
    {
        TrackHead();
    }

    public void Stand()
    {
        standPos = head.transform.pos;
    }

    public void SaveDate()
    {
        //クラスに放り込む
        IntialDate intialDate;
        intialDate = new IntialDate();
        intialDate.Jump = jumpMaxPos;
        intialDate.Stand = standPos;
        intialDate.Crouch = crouchPos;

        CalibEvent.Invoke(intialDate); //イベント発行
        Debug.Log("Saved!!");
    }

    void SceneUnloaded (Scene thisScene)
    {
        Debug.Log("シーンが変わります");
    }

    void TrackHead()
    {
        Text calibtext = calibText.GetComponent<Text>();

        SteamVR_Utils.RigidTransform Head_tr = head.transform;
        headSpeed = head.velocity.magnitude;

        calibtext.text = "位置:" + Head_tr.pos + "\n速度:" + headSpeed.ToString("F2"); // 画面上に表示

        //------------------------------比較-------------------------------
        //最大
        if (jumpMaxPos.y < Head_tr.pos.y)
        {
            jumpMaxPos = Head_tr.pos;
        }
        //最小
        if (crouchPos.y > Head_tr.pos.y)
        {
            crouchPos = Head_tr.pos;
        }
        //-----------------------比較ここまで-------------------------------
    }
}

//データ格納用クラス
public class IntialDate
{
    public Vector3 Jump;
    public Vector3 Stand;
    public Vector3 Crouch;
}
