using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContJumpRig : MonoBehaviour {

    public GameObject player;
    Rigidbody rb;

    //落ちる速さ、重力
    public float gravity = 10f;
    //ジャンプ力
    private float jumpSpeed = 4f;

    //Playerの移動や向く方向を入れる
    Vector3 moveDirection;

    //ジャンプボタン押している時間を判定
    //その時間を制限するため
    [SerializeField] float jumpTime;

    void Start()
    {
        player = GameObject.Find("Player");
        //CharacterControllerを取得
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Playerが地面に設置していることを判定
        if (Monitor.isGround)
        {
            jumpTime = 0;
        }

        //コントローラー振っていると上昇
        //振っている時間1秒まで、jumpUpEndがfalseの場合有効
        if (FlagManager.Instance.flags[3] == true && jumpTime < 1f)
        {
            //ジャンプボタン押している秒数を加算
            jumpTime += Time.deltaTime;
            moveDirection.y = jumpSpeed;

            rb.velocity = new Vector3(0, 4, 0);
        }

        //ジャンプ中にコントローラー振るの止めたことを記録
        //jumpUpEndがfalseの場合有効
        if (FlagManager.Instance.flags[3] == false)
        {
            //jumpTime = 0;
        }
    }
}
