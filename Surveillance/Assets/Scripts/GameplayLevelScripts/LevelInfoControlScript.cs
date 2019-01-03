using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoControlScript : MonoBehaviour {

    Transform CanvasTransform;
    Vector3 InfoScreenPos;

	// Use this for initialization
	void Start ()
    {
        CanvasTransform = GameObject.Find("GUI Canvas").transform;
        InfoScreenPos = transform.position;

        //infoScreenOff();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void infoScreenOff()
    {
        transform.SetParent(null);
    }
    public void infoScreenOn()
    {
        transform.SetParent(CanvasTransform);
        transform.position.Set(InfoScreenPos.x, InfoScreenPos.y, InfoScreenPos.z);
    }
}
