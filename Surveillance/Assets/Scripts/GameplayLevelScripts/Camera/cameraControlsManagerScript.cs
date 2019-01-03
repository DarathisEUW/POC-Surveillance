using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControlsManagerScript : MonoBehaviour {

    public float mouseDragSpeed;

    cameraSwitchScript camSwitch;

    public bool viewControl;
    public bool zoomControl;

    public bool gestureCameraControls;
    public bool gestureGameControls;

    //GameObject cameraControlsPannel;
    //GameObject gameControlsPannel;
    //Vector3 cameraControlsPannelPos;
    //Vector3 gameControlsPannelPos;

    GUIOverlays GUI;

    bool controlOnBegan;
    public float gestureControlHold = 1.0f;
    public float gestureViewTimer;
    public float gestureZoomTimer;

    public cameraSetup camerasSetup;
	// Use this for initialization
	void Start ()
    {
        //cameraControlsPannel = GameObject.Find("Camera View Buttons Overlay");
        //gameControlsPannel = GameObject.Find("Gameplay Buttons Overlay");
        //
        //cameraControlsPannelPos = cameraControlsPannel.transform.position;
        //gameControlsPannelPos = gameControlsPannel.transform.position;
        GUI = GameObject.Find("GUI Canvas").GetComponent<GUIOverlays>();

        camSwitch = GameObject.Find("Cameras").GetComponent<cameraSwitchScript>();

        camerasSetup = transform.GetComponent<cameraSetup>();
        camerasSetup.startCameraTransforms(camSwitch.sceneCams);
        //camSwitch.cameraStartRotations();

        #if UNITY_ANDROID
                Debug.Log("Android Build Control System");
                TouchDragControl();
                ApplyZoomControl();
            //GUI.toggleAndroidControlSettingsPannels(true);
            //camSwitch.cameraStartTransforms();
        #else
                Debug.Log("PC Control System");
                MouseDragControl();
                ApplyZoomControl();
                GUI.toggleAndroidControlSettingsPannels(false);
            //camSwitch.cameraStartTransforms();
        #endif

    }

    // Update is called once per frame
    void Update ()
    {
        //gesture controls
        //if gesturecontrols true
            //detect touches for view control toggle and zoom toggle 2 fingers for pinch
                                                                    // hold 1 finger for camera view
            if(gestureCameraControls)
            {

                if(Input.touchCount == 1)
                {
                    Touch touchOne = Input.touches[0];
                    //bool controlOnBegan;
                    if(touchOne.phase == TouchPhase.Began) controlOnBegan = viewControl;
                    if (touchOne.phase == TouchPhase.Stationary) gestureViewTimer += Time.deltaTime;
                    if ((touchOne.phase == TouchPhase.Ended) || (touchOne.phase == TouchPhase.Moved)) gestureViewTimer = 0;

                    if ((gestureViewTimer >= gestureControlHold) && (controlOnBegan == viewControl)) viewControlToggle();
                }

                if(Input.touchCount == 2)
                {
                    Touch touchOne = Input.touches[0];
                    Touch touchTwo = Input.touches[1];

                    //bool controlOnBegan;
                    if ((touchOne.phase == TouchPhase.Began) && (touchTwo.phase == TouchPhase.Began)) controlOnBegan = zoomControl;
                    if ((touchOne.phase == TouchPhase.Stationary) && (touchTwo.phase == TouchPhase.Stationary)) gestureZoomTimer += Time.deltaTime;
                    if (((touchOne.phase == TouchPhase.Ended) || (touchOne.phase == TouchPhase.Moved)) && ((touchTwo.phase == TouchPhase.Ended) || (touchTwo.phase == TouchPhase.Moved))) gestureZoomTimer = 0;

                    if ((gestureZoomTimer >= gestureControlHold) && (controlOnBegan == zoomControl)) zoomControlToggle();
                }
            }

    }
    // controls toggle for view or zoom
    public void viewControlToggle()
    {
        viewControl = !viewControl;
    }
    public void zoomControlToggle()
    {
        zoomControl = !zoomControl;
    }

    //toggles pannels of buttons for gesture/button control
    public void cameraControlSystemToggle()
    {
        gestureCameraControls = !gestureCameraControls;

        GUI.ToggleCameraControlsGUI(gestureCameraControls);
        //if (!gestureCameraControls)
        //{
        //    cameraControlsPannel.transform.parent = GameObject.Find("GUI Overlay").transform;
        //    cameraControlsPannel.transform.position = cameraControlsPannelPos;
        //}
        //else cameraControlsPannel.transform.parent = null;
    }
    public void gameplayControlSystemToggle()
    {

        gestureGameControls = !gestureGameControls;

        GUI.toggleGameControlsGUI(gestureGameControls);
        //if (!gestureGameControls)
        //{
        //    gameControlsPannel.transform.parent = GameObject.Find("GUI Overlay").transform;
        //    gameControlsPannel.transform.position = gameControlsPannelPos;
        //}
        //else gameControlsPannel.transform.parent = null;
    }

    //control system scripts
    public void ClearControls()
    {
        foreach (Camera cam in camSwitch.sceneCams)
        {
            Debug.Log("Removing Camera controls attached to Camera.");
            if (cam.GetComponent<gyroCameraScript>())       Destroy(cam.GetComponent<gyroCameraScript>());
            if (cam.GetComponent<touchDragControlScript>()) Destroy(cam.GetComponent<touchDragControlScript>());
            if (cam.GetComponent<VRCameraControl>())        Destroy(cam.GetComponent<VRCameraControl>());
        }
    }

    public void MouseDragControl()
    {
        camSwitch.camerasList();
        ClearControls();

        foreach(Camera cam in camSwitch.sceneCams)
        {
            Debug.Log("Adding Mouse Control to Camera: " + cam.name);
            cam.gameObject.AddComponent<mouseCameraControlScript>().speed = mouseDragSpeed;
        }
    }
    public void TouchDragControl()
    {
        camSwitch.camerasList();
        ClearControls();

        foreach (Camera cam in camSwitch.sceneCams)
        {
            Debug.Log("Removing Camera controls attached to Camera.");
            cam.gameObject.AddComponent<touchDragControlScript>();
        }
    }
    public void GyroControl()
    {
        camSwitch.camerasList();
        ClearControls();

        foreach (Camera cam in camSwitch.sceneCams)
        {
            Debug.Log("Adding Gyro Controls to camera: " + cam.name);
            cam.gameObject.AddComponent<gyroCameraScript>();
        }
    }
    public void VRControl()
    {
        camSwitch.camerasList();
        ClearControls();

        foreach (Camera cam in camSwitch.sceneCams)
        {
            Debug.Log("Adding Gyro Controls to camera: " + cam.name);
            cam.gameObject.AddComponent<VRCameraControl>();
        }
    }

    public void ApplyZoomControl()
    {
        camSwitch.camerasList();

        foreach (Camera cam in camSwitch.sceneCams)
        {
            Debug.Log("Adding Zoom Controls to camera: " + cam.name);
            cam.gameObject.AddComponent<zoomControlScript>();
        }
    }
}
