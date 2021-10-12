using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContJump : MonoBehaviour {

    //落ちる速さ、重力
    public float gravity = 10f;
    //ジャンプ力
    private float jumpSpeed = 5f;

    //Playerの移動や向く方向を入れる
    Vector3 moveDirection;

    //ジャンプボタン離したことを判定
    //二段ジャンプを防ぐため
    private bool jumpUpEnd = false;

    //ジャンプボタン押している時間を判定
    //その時間を制限するため
    [SerializeField] float jumpTime;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Playerが地面に設置していることを判定
        if (FlagManager.Instance.flags[0] == true)
        {
            // Y軸方向にジャンプさせる
            if (FlagManager.Instance.flags[3] == true)
                moveDirection.y = jumpSpeed;

            //ジャンプ中に関わる変数
            //地面についている時にリセット
            jumpUpEnd = false;
            jumpTime = 0;
        }
        else
        {
            //ジャンプボタン押していると上昇
            //押している時間3秒まで、jumpUpEndがfalseの場合有効
            if (FlagManager.Instance.flags[3] == true && jumpTime < 3f) //&& !jumpUpEnd)
            {
                //ジャンプボタン押している秒数を加算
                jumpTime += Time.deltaTime;
                moveDirection.y = jumpSpeed;
            }

            //ジャンプ中にジャンプボタン離したことを記録
            //jumpUpEndがfalseの場合有効
            if (FlagManager.Instance.flags[3] == true && !jumpUpEnd)
            {
                //二回ジャンプできなくする
                jumpUpEnd = true;
            }
        }

        // 重力を設定しないと落下しない
        moveDirection.y -= gravity * Time.deltaTime;

        // Move関数に代入する
        //rb.transform.position = moveDirection * Time.deltaTime;
    }
}
