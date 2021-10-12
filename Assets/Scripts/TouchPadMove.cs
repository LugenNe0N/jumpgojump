using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPadMove : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public enum MoveType
    {
        Walk,
        WalkOnClick,
        RotateOnClick,
    }

    public MoveType moveType;

    [SerializeField]
    private float Speed;
    [SerializeField]
    private Vector3 ChangeAngle;

    [SerializeField]
    private GameObject CameraRig;
    [SerializeField]
    private GameObject CameraEye;


    #region TouchPadProperty
    private bool TouchPadUp;
    private bool TouchPadDown;
    private bool TouchPadLeft;
    private bool TouchPadRight;
    #endregion


    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        if (moveType == MoveType.Walk)
        {
            if (Controller.GetAxis() != Vector2.zero)
            {
                Walk(Controller.GetAxis());
            }
        }
        else if (moveType == MoveType.WalkOnClick)
        {
            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (Controller.GetAxis() != Vector2.zero)
                {
                    Walk(Controller.GetAxis());
                }
            }
        }
        else if (moveType == MoveType.RotateOnClick)
        {
            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                Rotate();
            }
        }


        if (Controller.GetAxis() != Vector2.zero)
        {
            TouchPadPositionCheck(Controller.GetAxis());
        }
    }


    public Vector2 RotateMatrix(Vector2 TouchPosition)
    {
        float angle = (-CameraEye.transform.localEulerAngles.y - CameraRig.transform.localEulerAngles.y) * Mathf.Deg2Rad;
        float x = TouchPosition.x * Mathf.Cos(angle) - TouchPosition.y * Mathf.Sin(angle);
        float y = TouchPosition.x * Mathf.Sin(angle) + TouchPosition.y * Mathf.Cos(angle);

        Vector2 RotatedVector = new Vector2(x, y);

        return RotatedVector;
    }

    private void TouchPadPositionCheck(Vector2 TouchPosition)
    {
        float degree = Mathf.Atan2(TouchPosition.x, TouchPosition.y) * Mathf.Rad2Deg;


        if (-30 < degree && degree < 30)
        {
            TouchPadUp = true;
        }
        else
        {
            TouchPadUp = false;
        }

        if (-180 < degree && degree < -150 || 150 < degree && degree < 180)
        {
            TouchPadDown = true;
        }
        else
        {
            TouchPadDown = false;
        }

        if (30f < degree && degree < 150f)
        {
            TouchPadRight = true;
        }
        else
        {
            TouchPadRight = false;
        }

        if (-150f < degree && degree < -30f)
        {
            TouchPadLeft = true;
        }
        else
        {
            TouchPadLeft = false;
        }
    }


    private void Walk(Vector2 TouchPosition)
    {
        Vector2 RotatedVector = RotateMatrix(TouchPosition);
        Vector3 MovePosition = new Vector3(RotatedVector.x * Speed, 0, RotatedVector.y * Speed);
        CameraRig.transform.position += MovePosition;
    }

    private void Rotate()
    {
        if (TouchPadRight)
        {
            CameraRig.transform.localEulerAngles += ChangeAngle;
        }
        if (TouchPadLeft)
        {
            CameraRig.transform.localEulerAngles -= ChangeAngle;
        }
    }
}