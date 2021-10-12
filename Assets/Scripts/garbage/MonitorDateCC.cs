using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorDateCC : MonoBehaviour {
    public float Len;
    public float Move;
    public float Threshold;

    

    public void MonitorCCEvent(MDateCC monitorDateCC)
    {
        Len = monitorDateCC.mJumpPos.y; // 飛んだときの頭最大位置
        float Stand = 1.6f; //CalibDate.Stand.y; // 設定時の立ち
        float Jump = 1.9f; // CalibDate.Jump.y; // 設定時の飛んだときの高さ
        Move = Jump - Stand; //頭の移動距離
        Threshold = Stand + Move * 2 / 3; // 閾値


        //Debug.Log("Len  ：" + Len + "\n"
        //        + "Stand：" + Stand + "\n"
        //        + "Jump ：" + Jump + "\n"
        //        + "Move ：" + Move + "\n"
        //        + "Threshold：" + Threshold + "\n");

        //大
        if (Len >= Threshold)
        {
            MonitorCC.jumpSpeed = 10.0f;
            Debug.Log("大ジャンプ");
        }
        //小
        else if (Threshold > Len)
        {
            MonitorCC.jumpSpeed = 8.0f;
            Debug.Log("通常ジャンプ");
        }
    }
}
