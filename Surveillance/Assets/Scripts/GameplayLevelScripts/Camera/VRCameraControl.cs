using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraControl : MonoBehaviour {

    public bool viewControlEnabled;

    bool calibra;
    public float initialYAngle;
    public float appliedGyroYAngle;
    public float calibratedYAngle;

    // Use this for initialization
    void Start ()
    {
        Input.gyro.enabled = true;
        calibra = true;
        initialYAngle = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update ()
    {
        viewControlEnabled = GameObject.Find("Cameras").GetComponent<cameraControlsManagerScript>().viewControl;

        if (calibra)
        {
            calibrateYAngle();
            calibra = false;
        }
        applyGyroPosition();
        applyCalibration();
    }

    void calibrateYAngle()
    {
        calibratedYAngle = appliedGyroYAngle - initialYAngle;
    }
    void applyGyroPosition()
    {
        transform.rotation = Input.gyro.attitude;
        transform.Rotate(0f, 0f, 180f, Space.Self);
        transform.Rotate(90f, 180f, 0f, Space.World);
        appliedGyroYAngle = transform.eulerAngles.y;
    }
    void applyCalibration()
    {
        transform.Rotate(0f, -calibratedYAngle, 0f, Space.World);
    }
}