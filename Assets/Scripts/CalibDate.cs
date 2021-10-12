using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibDate : MonoBehaviour {
    //基準データ保持用
    public static Vector3 Jump;
    public static Vector3 Stand;
    public static Vector3 Crouch;

    public float jumpY;
    public float standY;
    public float crouchY;

    public void CalibEvent(IntialDate intialDate)
    {
        Jump = intialDate.Jump;
        Stand = intialDate.Stand;
        Crouch = intialDate.Crouch;

        Debug.Log("Event Init!!");

        Debug.Log("ジャンプ：" + Jump + "Y；" +jumpY
                + "\nスタンド：" + Stand + "Y；" + standY
                + "\nしゃがみ：" + Crouch + "Y；" + crouchY);
    }
}
