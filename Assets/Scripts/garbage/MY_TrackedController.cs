using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class MY_TrackedController : MonoBehaviour
{
    SteamVR_TrackedObject trackedController;
    public float dvSpeed; //デバイス速度
    public float befSpeed; //1つ前のFrameでの速度
    [SerializeField] float clickTime;
    GameObject player;
    CharacterController characontroller;
    void Start()
    {
        player = GameObject.Find("Player");
        characontroller = player.GetComponent<CharacterController>();
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

        //コントローラージャンプ操作
        if (!characontroller.isGrounded) {
            if (dvSpeed > 0.09 || befSpeed > 0.09)
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
        befSpeed = dvSpeed;
    }
}