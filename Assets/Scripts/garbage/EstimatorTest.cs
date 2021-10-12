using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class EstimatorTest : MonoBehaviour {

    Vector3 speed;
    Vector3 accel;
    Vector3 angular;

    public GameObject messageText = null;
    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Text text = messageText.GetComponent<Text>();

        VelocityEstimator VE = GetComponent<VelocityEstimator>();
        speed = VE.GetVelocityEstimate();
        accel = VE.GetAccelerationEstimate();
        angular = VE.GetAngularVelocityEstimate();

        text.text = "速度：" + speed + "\n" + " 加速度：" + accel + "\n" + " 角速度：" + angular;
	}
}
