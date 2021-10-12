using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadMonitor : MonoBehaviour
{

    SteamVR_Controller.Device head = null;
    Text[] texts = null;

    float fps = 0;
    float fpsTime = 0;

    Vector3 latestPos;
    Vector3 speed;

    //------------------------------------------------------------------------------------------------------------------------------//
    void Start()
    {

        // グローバルからHMDデバイスの参照を取得
        head = SteamVR_Controller.Input((int)SteamVR_TrackedObject.EIndex.Hmd);

        texts = transform.Find("Inputs").GetComponentsInChildren<Text>();
        fpsTime = Time.time;
    }

    void Update()
    {

        // FPSをトラックする
        TrackFPS();

        // Headの状態をトラックする
        TrackHead();

        //speed = ((head.transform.position - latestPos) / Time.deltaTime).magnitude;
        //latestPos = head.transform.position;
    }

    //------------------------------------------------------------------------------------------------------------------------------//
    void TrackFPS()
    {
        fps += 1;
        if (Time.time >= fpsTime)
        {
            texts[0].text = "FPS : " + fps + "";
            fps = 0;
            fpsTime += 1;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------//
    void TrackHead()
    {
        SteamVR_Utils.RigidTransform Head_tr = head.transform;
        texts[1].text = "Position : " + Head_tr.pos + "";
        texts[2].text = "Rotation : (" + Mathf.RoundToInt(Head_tr.rot.eulerAngles.x) + " : " + Mathf.RoundToInt(Head_tr.rot.eulerAngles.y) + " : " + Mathf.RoundToInt(Head_tr.rot.eulerAngles.z) + ")";
    }
}