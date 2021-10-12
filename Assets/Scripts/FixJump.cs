using System.Collections;
using System.Collections.Generic;
using UnityEngine;
////CharacterControllerを入れる
//[RequireComponent(typeof(CharacterController))]

public class FixJump : MonoBehaviour
{
    //CharacterControllerを変数にする
    CharacterController controller;

    //落ちる速さ、重力
    public float gravity = 10f;
    //ジャンプ力
    private float jumpSpeed = 5f;

    //Playerの移動や向く方向を入れる
    Vector3 moveDirection;

    public bool Buttonflg;

    void Start()
    {
        //CharacterControllerを取得
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Playerが地面に設置していることを判定
        if (controller.isGrounded)
        {
            // Y軸方向にジャンプさせる
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("JumpButton Pressed!!");
                Buttonflg = true;
            }
        }
        else
        {
            Buttonflg = false;
        }

        if (Buttonflg == true)
        {
            moveDirection.y = jumpSpeed;
        }

        // 重力を設定しないと落下しない
        moveDirection.y -= gravity * Time.deltaTime;

        // Move関数に代入する
        controller.Move(moveDirection * Time.deltaTime);
    }
}
