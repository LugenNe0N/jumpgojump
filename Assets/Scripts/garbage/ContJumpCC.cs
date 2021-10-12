using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CharacterControllerを入れる
[RequireComponent(typeof(CharacterController))]

public class ContJumpCC : MonoBehaviour {

    //CharacterControllerを変数にする
    CharacterController controller;

    //落ちる速さ、重力
    public float gravity = 20f;
    //ジャンプ力
    private float jumpSpeed = 2f;

    //Playerの移動や向く方向を入れる
    Vector3 moveDirection;

    //ジャンプボタン押している時間を判定
    //その時間を制限するため
    [SerializeField] float jumpTime;

    int count;

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
            if (FlagManager.Instance.flags[3] == true)
                moveDirection.y = jumpSpeed;

            //ジャンプ中に関わる変数
            //地面についている時にリセット
            //jumpUpEnd = false;
            jumpTime = 0;
        }
        else
        {
            //コントローラー振っていると上昇
            //振っている時間1秒まで、jumpUpEndがfalseの場合有効
            if (FlagManager.Instance.flags[3] == true && jumpTime < 1f)
            {
                //ジャンプボタン押している秒数を加算
                jumpTime += Time.deltaTime;
                moveDirection.y = jumpSpeed;
            }

            //ジャンプ中にコントローラー振るの止めたことを記録
            //jumpUpEndがfalseの場合有効
            if (FlagManager.Instance.flags[3] == false)
            {

            }
        }

        // 重力を設定しないと落下しない
        moveDirection.y -= gravity * Time.deltaTime;

        // Move関数に代入する
        controller.Move(moveDirection * Time.deltaTime);
    }
}
