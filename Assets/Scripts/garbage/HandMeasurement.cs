using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMeasurement : MonoBehaviour
{

    SteamVR_TrackedObject trackedController;
    public SteamVR_Utils.RigidTransform handPos;
    public float handSpeed;

    // Use this for initialization
    void Start()
    {
        trackedController = gameObject.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        var head = SteamVR_Controller.Input((int)trackedController.GetComponent<SteamVR_TrackedObject>().index);
        handPos = head.transform;
        handSpeed = head.velocity.magnitude;
    }
}

