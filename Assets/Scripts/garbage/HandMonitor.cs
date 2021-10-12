using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandMonitor : MonoBehaviour
{

    SteamVR_Controller.Device hand = null;
    Text[] texts = null;
    bool evflag = false; 

    //------------------------------------------------------------------------------------------------------------------------------//
    void Start()
    {

        //親オブジェクトにアタッチされているコントローラデバイスの参照を取得
        SteamVR_TrackedObject handTO = transform.parent.GetComponent<SteamVR_TrackedObject>();
        hand = SteamVR_Controller.Input((int)handTO.index);
        texts = transform.Find("Inputs").GetComponentsInChildren<Text>();
    }

    void Update()
    {

        // コントローラの状態をトラックする
        TrackHand();

        // 各ボタンからの入力をトラックする
        TrackInputs();
    }

    //------------------------------------------------------------------------------------------------------------------------------//
    void TrackHand()
    {
        SteamVR_Utils.RigidTransform Hand_tr = hand.transform;
        texts[0].text = "Position : " + Hand_tr.pos + "";
        texts[1].text = "Rotation : (" + Mathf.RoundToInt(Hand_tr.rot.eulerAngles.x) + " : " + Mathf.RoundToInt(Hand_tr.rot.eulerAngles.y) + " : " + Mathf.RoundToInt(Hand_tr.rot.eulerAngles.z) + ")";
    }

    void TrackInputs()
    {
        string ev = null;
        TrackTrigger(ref ev);
        TrackTouchPad(ref ev);
        TrackGrip(ref ev);
        TrackButtons(ref ev);
        if (ev != null)
        {
            texts[2].text = "Input : " + ev;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------//
    void TrackTrigger(ref string ev)
    {

        if (hand.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            ev = "Trigger-GetTouchDown";
        }
        if (hand.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            ev = "Trigger-GetPressDown";
            evflag = true;
        }
        if (hand.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            ev = "Trigger-GetTouchUp";
        }

        //トリガーの深さを取得
        var value = hand.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        texts[3].text = "Trigger Depth : " + value.x;
    }

    void TrackTouchPad(ref string ev)
    {

        if (hand.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            ev = "Touchpad-GetTouchDown";
        }
        if (hand.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            ev = "Touchpad-GetTouchUp";
        }
        if (hand.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            ev = "Touchpad-GetPressDown";
        }

        //タッチパッドのタッチ位置を取得
        var value = hand.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        texts[4].text = "Touchpad Position : " + value;
    }

    void TrackGrip(ref string ev)
    {
        if (hand.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            ev = "Grip-GetPressDown";
        }
    }

    void TrackButtons(ref string ev)
    {
        if (hand.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            ev = "ApplicationMenu-GetPressDown";
        }
    }
}
