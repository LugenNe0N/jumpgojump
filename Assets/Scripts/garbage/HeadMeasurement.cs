using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class HeadMeasurement : MonoBehaviour {

    SteamVR_Controller.Device head = null;
    public float headSpeed;
    public float length;
    public Vector3 maxPos = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 minPos = new Vector3(0.0f,0.0f,0.0f);
    public float maxSpeed = 0;
    public float minSpeed = 0;

    string filePath;
    string maxP;
    string minP;
    string maxS;
    string minS;

    // Use this for initialization
    void Start () {
        head = SteamVR_Controller.Input((int)SteamVR_TrackedObject.EIndex.Hmd);
        minPos = head.transform.pos;

        filePath = Application.dataPath + @"\File\MinMax.txt";

        //　ファイルが存在しなければ作成
        if (!File.Exists(filePath))
        {
            using (File.Create(filePath))
            {
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        TrackHead();
        minP = minPos.ToString();
        maxP = maxPos.ToString();
        minS = minSpeed.ToString();
        maxS = maxSpeed.ToString();
    }

    void LateUpdate()
    {
        string[] MinMax = {"MaxPosition:" + maxP, "MinPosition:" + minP, "Length" + length,"MaxSpeed:" + maxS, "MinSpeed:" + minS };
        File.WriteAllLines(filePath, MinMax);
    }

    void TrackHead()
    {
        SteamVR_Utils.RigidTransform Head_tr = head.transform;
        length = Head_tr.pos.magnitude;
        headSpeed = head.velocity.magnitude;

        Debug.Log("Position:" + Head_tr.pos + " ,Speed:" + headSpeed + " ,Length" + length);


        if (maxPos.y < Head_tr.pos.y)
        {
            maxPos = Head_tr.pos;
            //Debug.Log(maxPos);
        }
        if (minPos.y > Head_tr.pos.y)
        {
            minPos = Head_tr.pos;
        }

        if (maxSpeed < headSpeed)
        {
            maxSpeed = headSpeed;
        }
        if (minSpeed > headSpeed)
        {
            minSpeed = headSpeed;
        }
    }
}
