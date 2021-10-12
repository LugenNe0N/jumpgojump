using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class Monitor : MonoBehaviour
{
    //イベント設定
    /*
    [Serializable] public class EventInt : UnityEvent<MDate> { }
    [SerializeField] EventInt MonitorEvent;
    */

    [SerializeField] UnityEvent JumpEvent;
    [SerializeField] UnityEvent bigjumpEvent;

    //変数宣言
    SteamVR_Controller.Device head = null;
    public float monitorHeadSpeed;
    public static Vector3 MonitorJump = new Vector3(0.0f, 0.0f, 0.0f); //最大位置
    public Vector3 MonitorCrouch = new Vector3(0.0f, 0.0f, 0.0f); //最小位値

    public Vector3 beforeVelo; //前フレームのベクトル
    public Vector3 headAccel; //加速度ベクトル
    public float headAccelLen; //加速度ベクトルの長さ
    public float MaxAccel; //最大加速度

    //データ参照用
    public Vector3 intJump;
    public Vector3 intStand;

    public GameObject moniText = null;
    public GameObject cdateText = null;

    //ジャンプ確認用
    public GameObject tama;

    //テキスト表示用
    public Text cdatetext;

    //ジャンプアクション用
    public GameObject player;
    private Rigidbody playerRb;

    //レイ
    float distance = 1.1f; // レイを飛ばす距離
    [SerializeField] private Transform rayPosition; // レイを飛ばす位置
    public static bool isGround = false; //　レイが地面に到達しているかどうか
    
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
        intStand = CalibDate.Stand;
        //初期データ表示
        cdatetext.text = "ジャンプ：" + intJump + "\n立ち：" + intStand;
        
        //ジャンプさせるオブジェクト取得
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody>();

        if (JumpEvent == null)JumpEvent = new UnityEvent();
        if (bigjumpEvent == null)bigjumpEvent = new UnityEvent();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TrackHead();
        ChangeColor();

        cdatetext.text = "ジャンプ：" + intJump + "\n立ち：" + intStand;

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

            FlagManager.Instance.flags[5] = false;
            FlagManager.Instance.flags[6] = false;
            //Debug.Log("接地");
        }
        else
        {
            isGround = false;
            //Debug.Log("空中");
        }
        /*---------------------------------------------------------------------------------------------*/

        if (FlagManager.Instance.flags[0] == false) //接地していない
        {
            if (10 < MaxAccel && MaxAccel < 14)
            {
                if (FlagManager.Instance.flags[2] == true)
                {
                    //Debug.Log("小ジャンプ");
                    //playerRb.velocity = new Vector3(0.0f, 5.0f, 0.0f);

                    JumpEvent.Invoke();

                    FlagManager.Instance.flags[2] = false;
                }
            }else if (MaxAccel > 16)
            {
                if (FlagManager.Instance.flags[2] == true)
                {
                    //Debug.Log("大ジャンプ");
                    //playerRb.velocity = new Vector3(0.0f, 8.0f, 0.0f);

                    bigjumpEvent.Invoke();

                    FlagManager.Instance.flags[2] = false;
                }
            }

            
        }
        else //着地時リセット
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                playerRb.velocity = new Vector3(0.0f, 5.0f, 0.0f);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerRb.velocity = new Vector3(0.0f, 8.0f, 0.0f);
            }
            FlagManager.Instance.flags[2] = true;
            MaxAccel = 0f;
            MonitorJump = intStand; //立ち位置に最大値初期化
        }
    }

    void TrackHead()
    {
        SteamVR_Utils.RigidTransform Head_tr = head.transform;
        monitorHeadSpeed = head.velocity.magnitude; //頭の移動速度

        //頭の加速度計算
        headAccel = (head.velocity - beforeVelo) / Time.fixedDeltaTime;
        headAccelLen = headAccel.magnitude;

        //ゲーム中に表示
        Text monitext = moniText.GetComponent<Text>();
        monitext.text = "位置:" + Head_tr.pos + "\n最大位置:" + MonitorJump + "\n加速度:" + Math.Floor(MaxAccel); //【正面】
        
        //立ち位置リセット
        if (FlagManager.Instance.flags[4] == true) //コントローラーのメニューボタン押されたかフラグ
        {
            CalibDate.Stand = Head_tr.pos;
            intStand = CalibDate.Stand;
            Raycast.distance = CalibDate.Stand.y + 0.01f;
        }
        
        //最大位置を更新
        if (MonitorJump.y < Head_tr.pos.y)
        {
            MonitorJump = Head_tr.pos;
        }

        //最大加速度を更新
        if (MaxAccel < headAccelLen)
        {
            MaxAccel = headAccelLen;
        }

        beforeVelo = head.velocity;
        
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