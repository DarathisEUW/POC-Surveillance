using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitchScript : MonoBehaviour {

    Transform cameras;
    public List<Camera> sceneCams;

    public int activeCam;

    //public Transform Camera1StartTransform;
    //public Transform Camera2StartTransform;
    //public Transform Camera3StartTransform;

	// Use this for initialization
	void Start ()
    {

        activeCam = 0;

        // done in manager for porpoise of hacksing leetly
        camerasList();

        //Camera1StartTransform = GameObject.Find("Camera1").transform;
        //Camera2StartTransform = GameObject.Find("Camera2").transform;
        //Camera3StartTransform = GameObject.Find("Camera3").transform;
        //
        //cameraStartTransforms();

	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < sceneCams.Count; i++)
        {
            if (i == activeCam)
            {
                sceneCams[i].enabled = true;
                sceneCams[i].GetComponent<AudioListener>().enabled = true;
            }
            else
            {
                sceneCams[i].enabled = false;
                sceneCams[i].GetComponent<AudioListener>().enabled = false;
            }
        }
	}
    public void camerasList()
    {
        Debug.Log("Getting Cameras List");

        cameras = GameObject.Find("Cameras").transform;
        sceneCams.Clear();
        foreach (Transform obj in cameras) sceneCams.Add(obj.GetComponent<Camera>());
    }

    public void nextCam()
    {
        ++activeCam;
        if (activeCam > (sceneCams.Count) - 1) activeCam = 0;
    }
    public void prevCam()
    {
        --activeCam;
        if (activeCam < 0) activeCam = sceneCams.Count - 1;
    }
    //public void cameraStartTransforms()
    //{
    //    Debug.Log("Rotating Cameras to good start rotations");
    //    sceneCams[0].GetComponent<Transform>() = CameraStart1Transform;
    //    sceneCams[1].transform = CameraStart2Transform;
    //    sceneCams[2].transform = CameraStart3Transform;
    //}
}
