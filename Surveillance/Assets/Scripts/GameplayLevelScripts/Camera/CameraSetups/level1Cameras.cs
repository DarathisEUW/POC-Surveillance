using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1Cameras : cameraSetup {

    //public List<Transform> CamStartTransforms;
    public Vector3 cam1Pos, cam2Pos, cam3Pos;
    public Vector2 cam1Rot, cam2Rot, cam3Rot;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void startCameraTransforms(List<Camera> cams)
    {
        Debug.Log("Setting start camera rotations");
        //cams[0].transform.localPosition = cam1Pos;
        //cams[0].transform.eulerAngles = new Vector3(cam1Rot.x, cam1Rot.y, 0);

        //cams[1].transform.position = cam2Pos;
        //cams[1].transform.eulerAngles = new Vector3(cam2Rot.x, cam2Rot.y, 0);

        //cams[2].transform.position = cam3Pos;
        //cams[2].transform.eulerAngles = new Vector3(cam3Rot.x, cam3Rot.y, 0);
    }

}
