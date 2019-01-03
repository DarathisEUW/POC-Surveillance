using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroCameraScript : MonoBehaviour
{

    public bool viewControlEnabled;


    public Vector2 touchOne;
    public Vector2 touchTwo;
    public float touchHold;
    public float touchTwoHold;

    public float holdDuration = 1.5f;



    public bool GyroZoomControl;

    public float pinchDistance;
    public float pinchDistLastFrame;

    public Vector2 firstFingerPosition;
    public Vector2 secondFingerPosition;

    Camera cam;

    public float zoomPinchExpandDelta;
    public float zoomSpeed = 2f;
    public float FOVMin = 20f;
    public float FOVCurrent;
    public float FOVMax = 70f;

    // Use this for initialization
    void Start ()
    {
        Input.gyro.enabled = true;

        cam = gameObject.GetComponent<Camera>();
	}

    // Update is called once per frame
    void Update()
    {
        viewControlEnabled = GameObject.Find("Cameras").GetComponent<cameraControlsManagerScript>().viewControl;

        if(viewControlEnabled && cam.isActiveAndEnabled)
        transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);

        // touch controls
        //if (Input.touchCount == 1) // hold for free gyro cam toggle
        //{
        //    touchOne = Input.GetTouch(0).position;
        //    touchTwo = new Vector2(0, 0);
        //
        //    touchHold += Time.deltaTime;
        //    if (touchHold >= holdDuration) viewControlEnabled = true;
        //
        //    //GyroCameraControl = true; // activates Gyro
        //    GyroZoomControl = false;
        //}
        //else if(Input.touchCount == 2) // pinch/zoom for 
        //{
        //    touchOne = Input.GetTouch(0).position;
        //    touchTwo = Input.GetTouch(1).position;
        //
        //    zoomPinchExpandDelta = 0;
        //
        //    pinchDistance = Vector2.Distance(touchOne, touchTwo);
        //    zoomPinchExpandDelta = pinchDistance - pinchDistLastFrame;
        //
        //    pinchDistLastFrame = pinchDistance;
        //
        //    touchTwoHold += Time.deltaTime;
        //    if (touchTwoHold >= holdDuration) GyroZoomControl = true;
        //
        //    //GyroZoomControl = true; // activates zooming
        //    viewControlEnabled = false;
        //}
        //else
        //{
        //    touchOne = new Vector2(0, 0);
        //    touchTwo = new Vector2(0, 0);
        //
        //    touchHold = 0f;
        //    touchTwoHold = 0.0f;
        //
        //    GyroZoomControl = false;
        //    viewControlEnabled = false;
        //}
        //
        //// if gyro cam control is toggled on
        //if (viewControlEnabled)
        //{
        //    transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
        //}
        //FOVCurrent = cam.fieldOfView;
        //
        //// if gyro cam zoom toggled on
        //if (GyroZoomControl)
        //{
        //    if (zoomPinchExpandDelta != 0)
        //    {
        //        if (zoomPinchExpandDelta > 0f)
        //        {
        //            if (FOVCurrent > FOVMin) cam.fieldOfView -= zoomSpeed; Debug.Log("Zooming In - " + cam.name + " FOV: " + cam.fieldOfView);
        //        }
        //        else// (zoomPinchExpandDelta < 0f)
        //        {
        //            if (FOVCurrent < FOVMax) cam.fieldOfView += zoomSpeed; Debug.Log("Zooming Out - " + cam.name + " FOV: " + cam.fieldOfView);
        //        }
        //    }
        //}
    }
}
