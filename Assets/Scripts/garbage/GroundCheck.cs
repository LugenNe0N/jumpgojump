using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private void Start()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        if (CalibDate.Stand.y == 0.0)
        {
            collider.radius = 1.6f;
        }
        else
        {
            collider.radius = CalibDate.Stand.y;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            FlagManager.Instance.flags[0] = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            FlagManager.Instance.flags[0] = false;
        }
    }
}
