using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class ResetPosition : MonoBehaviour {

    SteamVR_TrackedObject trackedObject;

	// Use this for initialization
	void Start () {
        trackedObject = gameObject.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)trackedObject.GetComponent<SteamVR_TrackedObject>().index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("メニューボタンをクリックした");
        }
    }
}