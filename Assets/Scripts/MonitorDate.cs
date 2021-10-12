using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MonitorDate : MonoBehaviour {

    public GameObject Player;
    public Rigidbody PRb;
    public bool JumpFlag;
    public bool BigJumpFlag;

    void Start()
    {
        Player = GameObject.Find("Player");
        PRb = Player.GetComponent<Rigidbody>();
        JumpFlag = false;
        BigJumpFlag = false;
    }

    void Update()
    {
        if (JumpFlag == true)
        {
            PRb.velocity = new Vector3(0.0f, 5.0f, 0.0f);
            JumpFlag = false;
        }
        if (BigJumpFlag == true)
        {
            PRb.velocity = new Vector3(0.0f, 8.0f, 0.0f);
            BigJumpFlag = false;
        }

    }

    public void JumpAction()
    {
        Debug.Log("JumpEvent!!");
        JumpFlag = true;
    }

    public void BigjumpAction()
    {
        Debug.Log("BigJumpEvent!!");
        BigJumpFlag = true;
    }
}
