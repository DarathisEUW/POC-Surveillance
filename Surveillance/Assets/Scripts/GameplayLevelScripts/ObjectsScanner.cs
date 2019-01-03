using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectsScanner : MonoBehaviour
{

    public Camera cam;
    public List<GameObject> ObjectsInView;

    Transform sceneObjs;
    cameraSwitchScript cameraSwitch;
    Plane[] planes;

    public List<GameObject> visibleObjects;

    int visiblePtsReq;

    // Use this for initialization
    void Start()
    {
        sceneObjs = GameObject.Find("Entities").transform;
        cameraSwitch = GameObject.Find("Cameras").GetComponent<cameraSwitchScript>();

    }

    // Update is called once per frame
    void Update()
    {
        cam = cameraSwitch.sceneCams[cameraSwitch.activeCam];
        planes = GeometryUtility.CalculateFrustumPlanes(cam);

        ObjectsInView.Clear();
        visibleObjects.Clear();

        //object is in view cone of active camera
        foreach (Transform obj in sceneObjs.transform)
        {
            if (GeometryUtility.TestPlanesAABB(planes, obj.gameObject.GetComponent<Collider>().bounds)) ObjectsInView.Add(obj.gameObject);
        }


        foreach (GameObject Obj in ObjectsInView)
        {
            int visiblePoints = 0;
            foreach (Transform child in Obj.transform)
            {
                RaycastHit hit;
                Vector3 pointDirection = child.transform.position - cam.transform.position;

                if (Physics.Raycast(cam.gameObject.transform.position, pointDirection, out hit))
                {
                    //if (hit.collider == Obj.GetComponent<Collider>()) Debug.DrawRay(cam.transform.position, pointDirection * hit.distance, Color.red);
                    //else Debug.DrawRay(cam.transform.position, pointDirection * hit.distance, Color.white);
                    if(hit.collider == Obj.GetComponent<Collider>()) visiblePoints++;
                }
            }

            Vector3 objDirection = Obj.transform.position - cam.transform.position;
            float objDistance = Vector3.Distance(cam.transform.position, Obj.transform.position);

            Obj.GetComponent<AIEntity>().visiblePoints = visiblePoints;

            if (GameObject.Find("GUI Canvas").GetComponent<GameplayScript>() != null) {
                visiblePtsReq = GameObject.Find("GUI Canvas").GetComponent<GameplayScript>().unobscuredPointsReq;
            }

            if (visiblePoints == Obj.transform.childCount) {
                Debug.DrawRay(cam.transform.position, objDirection * objDistance, Color.green);
                visibleObjects.Add(Obj);
            }
            else if (visiblePoints >= visiblePtsReq) { // >= 0
                Debug.DrawRay(cam.transform.position, objDirection * objDistance, Color.yellow);
                visibleObjects.Add(Obj);
            }
            else Debug.DrawRay(cam.transform.position, objDirection * objDistance, Color.red);
        }
    }
}
