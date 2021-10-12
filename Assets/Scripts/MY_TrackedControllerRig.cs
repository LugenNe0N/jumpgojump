using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class MY_TrackedControllerRig : MonoBehaviour
{
    //宣言
    SteamVR_TrackedObject trackedController;
    Vector3 lastVelocity;
    Vector3 acceleration;
    public float Accel;
    public float dvSpeed; //デバイス速度
    public float befSpeed; //1つ前のFrameでの速度
    [SerializeField] float clickTime;
    [SerializeField] private GameObject player;
    Rigidbody rb;

    void Start()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        trackedController = gameObject.GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedController.GetComponent<SteamVR_TrackedObject>().index);
        dvSpeed = device.velocity.magnitude;
     
        //立ち位置リセット操作
        if (device.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            clickTime += Time.deltaTime;
            if (clickTime > 1f)
            {
                FlagManager.Instance.flags[4] = true;
            }
        }
        else if(device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            clickTime = 0;
            FlagManager.Instance.flags[4] = false;
        }

        acceleration = (device.velocity - lastVelocity) / Time.fixedDeltaTime;
        Accel = acceleration.magnitude;

        //コントローラージャンプ操作
        if (!Monitor.isGround) {
            if (Accel > 30)// || befSpeed > 0.1)
            {
                FlagManager.Instance.flags[3] = true;
                //Debug.Log("Shaking !!");
            }
            else
            {
                FlagManager.Instance.flags[3] = false;
                //Debug.Log("Stop !!");
            }
        }

        lastVelocity = device.velocity;
        befSpeed = dvSpeed;
    }
}