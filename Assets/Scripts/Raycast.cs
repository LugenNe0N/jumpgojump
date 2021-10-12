using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public static float distance = 1.6f;
    public float jumpDistance = 0.0f;

    void Start()
    {
        distance = CalibDate.Stand.y;
        //Debug.Log("distance："+ distance);
        if (distance == 0)
        {
            distance = 1.6f;
        }
        FlagManager.Instance.flags[0] = false;
    }

    void Update()
    {
        //-----------------------接地判定用Ray----------------------------
        //Rayの作成
        Ray ray = new Ray(transform.position, -(transform.position - new Vector3(transform.position.x, -distance, transform.position.z)));

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの可視化
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue);

        //もしRayにオブジェクトが衝突かつ tagがGroundだったら
        if ((Physics.Raycast(ray, out hit, distance)) && hit.collider.tag == "Ground")
        {
            FlagManager.Instance.flags[0] = true;
            //Debug.Log("接地");
        }
        else
        {
            FlagManager.Instance.flags[0] = false;
            //Debug.Log("空中");
        }
    }
}

