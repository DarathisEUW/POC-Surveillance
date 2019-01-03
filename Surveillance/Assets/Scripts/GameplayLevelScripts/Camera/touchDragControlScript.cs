using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchDragControlScript : MonoBehaviour {

    public bool viewControlEnabled;

    Camera cam;

    //public Vector2 touchStartPos;
    //public Vector2 touchMovedPos;
    
    //float xAngleTemp;
    //float yAngleTemp;
    
    Vector3 firstPoint;
    Vector3 nowPoint;
    float xAngle;
    float yAngle;
    float xAngTemp;
    float yAngTemp;

    // Use this for initialization
    void Start ()
    {
        cam = gameObject.GetComponent<Camera>();


        firstPoint = new Vector3(0, 0, 0);
        nowPoint = new Vector3(0, 0, 0);

        xAngle = cam.transform.eulerAngles.x;
        yAngle = cam.transform.eulerAngles.y;
        cam.transform.eulerAngles = new Vector3(xAngle, yAngle, 0.0f);

        //cam.transform.rotation = Quaternion.Euler(xAngle, yAngle, 0.0f);
        //cam.transform.localEulerAngles = new Vector3(yAngle, xAngle, 0.0f);
        //Vector3 eulers = new Vector3(xAngle, yAngle);
        //cam.transform.rotation.SetEulerAngles(eulers);
    }

    // Update is called once per frame
    void Update ()
    {
        viewControlEnabled = GameObject.Find("Cameras").GetComponent<cameraControlsManagerScript>().viewControl;

        if (viewControlEnabled && cam.isActiveAndEnabled)
        {
            if (Input.touchCount == 1)
            {
                //Debug.Log("Update start - " + xAngle + " x/y " + yAngle);
                Touch touchOne = Input.GetTouch(0);
                
                if(touchOne.phase == TouchPhase.Began)
                {
                    Debug.Log("TouchStarted");
                    firstPoint = Input.GetTouch(0).position;
                    xAngTemp = xAngle;
                    yAngTemp = yAngle;
                }
                if (touchOne.phase == TouchPhase.Moved)
                {
                    Debug.Log("TouchMoved");
                    nowPoint = Input.GetTouch(0).position;

                    //yAngle = yAngTemp + (nowPoint.y - firstPoint.y) * 90.0f / Screen.height;
                    //xAngle = xAngTemp - (nowPoint.x - firstPoint.x) * 180.0f / Screen.width;

                    yAngle = yAngTemp + -(nowPoint.x - firstPoint.x) * 90.0f / Screen.height;
                    xAngle = xAngTemp - -(nowPoint.y - firstPoint.y) * 180.0f / Screen.width;

                    cam.transform.eulerAngles = new Vector3(xAngle, yAngle, 0.0f);

                }
                if(touchOne.phase == TouchPhase.Ended)
                {
                    Debug.Log("TouchEnded");
                    //touchStartPos = new Vector2(0, 0);
                    //touchMovedPos = new Vector2(0, 0);
                    //Debug.Log("TouchEnd: " + xAngle + " x/y " + yAngle);
                }
                //Debug.Log("Update fin - " + xAngle + " x/y " + yAngle);
            }
        }
    }
}
