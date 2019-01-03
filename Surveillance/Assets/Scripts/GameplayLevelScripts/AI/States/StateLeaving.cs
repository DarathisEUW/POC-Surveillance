using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLeaving : IState {

    AIEntity owner;
    public StateLeaving(AIEntity owner) { this.owner = owner; }
    //AIShopperScript AIShopper;

    //List<Transform> leavingPoints;
    Vector3 leavingPoint;

    public void onEnter()
    {
        //Debug.Log("Entering Leaving State");
        ////AIShopper = owner.GetComponent<AIShopperScript>();
        //leavingPoints = new List<Transform>();
        //Transform ObjectivePoints = GameObject.Find("ObjectivePoints").transform;
        //
        //Debug.Log("Searching for Despawn Points");
        //foreach (Transform ObjPoint in ObjectivePoints)
        //{
        //    if (ObjPoint.name == "Despawn") leavingPoints.Add(ObjPoint);
        //}
        //Debug.Log("Despawn Points: " + leavingPoints.Count);
        //
        //int randomLeave = Random.Range(0, leavingPoints.Count);
        //leavingPoint = leavingPoints[randomLeave].position;
        //Debug.Log("Leaving point: " + randomLeave + " at position: " + leavingPoints[randomLeave].position);
        leavingPoint = AIUtility.getRandom("Despawn").transform.position;

        owner.NavAgent.SetDestination(leavingPoint);
        //Debug.Log("Leaving shop, distance to exit = " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination));
        //Debug.Log("Set Leaving Destination");
    }
    public void Execute()
    {
        //Debug.Log("Leaving shop, distance to exit = " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination));
        //owner.NavAgent.SetDestination(leavingPoint);
        //if (AIShopper.navAgent.remainingDistance <= 0.5f) Debug.Log("I AM DED: " + owner.gameObject);
        //GameObject.Destroy(owner);
    }
    //public bool stateComplete;
    public bool stateComplete()
    {
        //Debug.Log("Checking State Complete for: StateLeaving");

        //if (AIShopper.navAgent.remainingDistance <= 0.5f)
        if (Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination) < 0.5f + owner.NavAgent.stoppingDistance + owner.GetComponent<Collider>().bounds.size.x)
            {
            //Debug.Log("Exit found: " + owner.gameObject);
            //Debug.Log(owner.gameObject + " - now despawning!");
            GameObject.Destroy(owner.gameObject);
            return true;
        }
        return false;
    }
    public void onExit()
    {
        //if (owner != null)
        //{
        //    if (stateComplete()) Debug.Log("Shopper off screen, left store, now despawning because hopefully out of view");
        //    else Debug.Log(owner.gameObject + " - Exiting StateLeaving aborted early (before despawn)!");
        //}
        //else Debug.Log("Aborted before AI Shopper definition!");
        //else Debug.Log("Shopper off screen, left store, now despawning because hopefully out of view");
    }
}
