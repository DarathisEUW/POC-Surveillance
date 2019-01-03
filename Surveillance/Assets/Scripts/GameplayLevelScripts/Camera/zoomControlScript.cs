using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomControlScript : MonoBehaviour {

    Camera cam;
    public bool zoomControlEnabled;

    public float zoomSpeed = 2f;
    public float FOVMin = 20f;
    public float FOVCurrent;
    public float FOVMax = 70f;

    public Vector2 touchOne;
    public Vector2 touchTwo;

    public float pinchDistance;
    public float pinchDistLastFrame;
    public float zoomPinchExpandDelta;

    public float mouseZoomDelta;

    // Use this for initialization
    void Start ()
    {
        cam = gameObject.GetComponent<Camera>();

        FOVCurrent = cam.fieldOfView;
    }
	
	// Update is called once per frame
	void Update ()
    {
        zoomControlEnabled = GameObject.Find("Cameras").GetComponent<cameraControlsManagerScript>().zoomControl;

        if (zoomControlEnabled && cam.isActiveAndEnabled)
        {
            FOVCurrent = cam.fieldOfView;

            if (Input.touchCount == 2)
            {
                touchOne = Input.GetTouch(0).position;
                touchTwo = Input.GetTouch(1).position;

                zoomPinchExpandDelta = 0;

                pinchDistance = Vector2.Distance(touchOne, touchTwo);
                zoomPinchExpandDelta = pinchDistance - pinchDistLastFrame;

                if (zoomPinchExpandDelta != 0)
                {
                    if (zoomPinchExpandDelta > 0f)
                    {
                        if (FOVCurrent > FOVMin) cam.fieldOfView -= zoomSpeed; //Debug.Log("Zooming In - " + cam.name + " FOV: " + cam.fieldOfView);
                    }
                    else// (zoomPinchExpandDelta < 0f)
                    {
                        if (FOVCurrent < FOVMax) cam.fieldOfView += zoomSpeed; //Debug.Log("Zooming Out - " + cam.name + " FOV: " + cam.fieldOfView);
                    }
                }
                pinchDistLastFrame = pinchDistance;
            }
            else
            {
                //camera zoom using scroll because ima basic betch
                float mouseZoomDelta = Input.GetAxis("Mouse ScrollWheel");

                if (mouseZoomDelta > 0.0f) // in
                {
                    if (FOVCurrent > FOVMin) cam.fieldOfView -= zoomSpeed; //Debug.Log("Zooming In - " + cam.name + " FOV: " + cam.fieldOfView);
                }
                else if (mouseZoomDelta < 0.0f) // out
                {
                    if (FOVCurrent < FOVMax) cam.fieldOfView += zoomSpeed; //Debug.Log("Zooming Out - " + cam.name + " FOV: " + cam.fieldOfView);
                }
                // try not to shake it all about
            }
        }
	}
}
