using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class MonitorCC : MonoBehaviour
{
    //イベント設定
    [Serializable] public class EventInt : UnityEvent<MDateCC> { }
    [SerializeField] EventInt MonitorCCEvent;

    //変数宣言
    SteamVR_Controller.Device head = null;
    public float monitorHeadSpeed;
    public Vector3 MonitorJump = new Vector3(0.0f, 0.0f, 0.0f); //最大位置初期化
    public Vector3 MonitorCrouch = new Vector3(0.0f, 0.0f, 0.0f); //最小位置初期化

    //データ参照用
    public Vector3 intJump;
    public Vector3 intStand;
    public Vector3 intCrouch;

    public GameObject moniText = null;
    public GameObject cdateText = null;

    //ジャンプ確認用
    public GameObject tama;

    //テキスト表示用
    public Text cdatetext;

    //ジャンプアクション用
    public GameObject player;
    CharacterController controller; //CharacterControllerを変数にする
    public static float jumpSpeed = 8.0f; //ジャンプ力
    public float gravity = 8.0f; //落ちる速さ、重力
    private Vector3 moveDirection; //Playerの移動や向く方向を入れる

    float distance = 1.1f;
    //　レイを飛ばす位置
    [SerializeField] private Transform rayPosition;
    //　レイが地面に到達しているかどうか
    private bool isGround = false;

    // Use this for initialization
    void Start()
    {
        //ジャンプフラグfalse
        FlagManager.Instance.flags[1] = false;
        FlagManager.Instance.flags[2] = true;

        head = SteamVR_Controller.Input((int)SteamVR_TrackedObject.EIndex.Hmd);
        MonitorCrouch = head.transform.pos;

        cdatetext = cdateText.GetComponent<Text>(); //テキスト表示

        //初期設定データ呼び出し
        intJump = CalibDate.Jump;
        intCrouch = CalibDate.Crouch;
        intStand = CalibDate.Stand;
        //表示
        cdatetext.text = "ジャンプ：" + intJump + "\n立ち：" + intStand + "\nしゃがみ：" + intCrouch;
        
        //ジャンプさせるオブジェクト取得
        player = GameObject.Find("Player");
        //CharacterControllerを取得
        controller = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TrackHead();
        ChangeColor();

        cdatetext.text = "ジャンプ：" + intJump + "\n立ち：" + intStand + "\nしゃがみ：" + intCrouch;

        if (!controller.isGrounded)
        {
            Debug.Log("NotisGround");
        }

        //　CharacterControllerのコライダで接地が確認出来ない場合--------------------------------------
        if (!controller.isGrounded)
        {
            //Rayの作成
            Ray ray = new Ray(new Vector3(rayPosition.position.x, rayPosition.position.y + 1.0f, rayPosition.position.z), -(rayPosition.position - new Vector3(rayPosition.position.x, -distance, rayPosition.position.z)));

            //Rayが当たったオブジェクトの情報を入れる箱
            RaycastHit hit;

            //Rayの可視化
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

            //もしRayにオブジェクトが衝突かつ tagがGroundだったら
            if ((Physics.Raycast(ray, out hit, distance)) && hit.collider.tag == "Ground")
            {
                isGround = true;
                //Debug.Log("接地");
            }
            else
            {
                isGround = false;
            }
        }
        /*---------------------------------------------------------------------------------------------*/

        if (FlagManager.Instance.flags[0] == false) //接地している
        {
            if (FlagManager.Instance.flags[1] == true) //特定速度以上で動いた
            {
                if (FlagManager.Instance.flags[2] == true) //1回目ですよ
                {
                    MDateCC monitorDateCC;
                    monitorDateCC = new MDateCC();
                    monitorDateCC.mJumpPos = MonitorJump;

                    MonitorCCEvent.Invoke(monitorDateCC); //イベント発行


                    FlagManager.Instance.flags[2] = false; //最初の一回折る

                    MonitorJump = intStand; //立ち位置に最大値初期化
                }
            }
        }
        else
        {
            FlagManager.Instance.flags[2] = true;
        }

        // 重力を設定しないと落下しない
        moveDirection.y -= gravity * Time.deltaTime;

        // Move関数に代入する
        controller.Move(moveDirection * Time.deltaTime);

        Debug.Log(moveDirection);
    }

    void TrackHead()
    {
        SteamVR_Utils.RigidTransform Head_tr = head.transform;
        monitorHeadSpeed = head.velocity.magnitude;

        Text monitext = moniText.GetComponent<Text>();
        monitext.text = "位置:" + Head_tr.pos + "\n最大位置:" + MonitorJump + "\n速度:" + monitorHeadSpeed.ToString("F2");

        //立ち位置リセット
        if (FlagManager.Instance.flags[4] == true) //コントローラーのメニューボタン押されたかフラグ
        {
            CalibDate.Stand = Head_tr.pos;
            intStand = CalibDate.Stand;
        }

        //最大位置　最小位置　更新
        if (MonitorJump.y < Head_tr.pos.y)
        {
            MonitorJump = Head_tr.pos;
        }
        if (MonitorCrouch.y > Head_tr.pos.y)
        {
            MonitorCrouch = Head_tr.pos;
        }

        //動いたか（飛んだか判定）
        if (monitorHeadSpeed > 1.0)
        {
            FlagManager.Instance.flags[1] = true; //動いたよフラグON
        }
        else
        {
            FlagManager.Instance.flags[1] = false; //動いたよフラグOFF
        }
    }

    void ChangeColor()
    {
        if (FlagManager.Instance.flags[0] == false && FlagManager.Instance.flags[1] == true)
        {
            tama.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (FlagManager.Instance.flags[0] == true && FlagManager.Instance.flags[1] == false)
        {
            tama.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}

public class MDateCC
{
    public Vector3 mJumpPos;
    public Vector3 mCrouchPos;
}