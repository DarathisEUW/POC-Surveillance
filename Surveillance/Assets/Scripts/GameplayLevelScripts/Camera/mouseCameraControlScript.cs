using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCameraControlScript : MonoBehaviour {

    Camera cam;
    public bool viewControlEnabled;

    public float speed = 3.5f;
    private float x;
    private float y;

    //public float zoomSpeed = 2f;
    //public float FOVMin = 20f;
    //public float FOVCurrent;
    //public float FOVMax = 70f;

	// Use this for initialization
	void Start ()
    {
        cam = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        viewControlEnabled = GameObject.Find("Cameras").GetComponent<cameraControlsManagerScript>().viewControl;

        if (cam.isActiveAndEnabled && viewControlEnabled)
        {
            // camera scroll pan whatnot control on right click
            if (Input.GetMouseButton(0)) // on right click
            {
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
                x = transform.rotation.eulerAngles.x;
                y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(x, y, 0);
            }
        }
	}
}
