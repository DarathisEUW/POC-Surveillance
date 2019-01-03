using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using System.Linq;

public class StateEntering : IState
{
    AIEntity owner;

    public StateEntering(AIEntity owner) { this.owner = owner;}
    //AIShopperScript AIShopper;

    Vector3 ShopEnterancePoint;


    public void onEnter()
    {
        //Debug.Log("Im entering Enter State!");

        //
        //Transform[] enterences = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Name");

        Transform closestEnterence = AIUtility.getClosest(owner.transform.position, "ShopEnterance");
        //Debug.Log(closestEnterence.transform.position);

        ShopEnterancePoint = closestEnterence.position;
        //AIShopper = owner.GetComponent<AIShopperScript>();


        //Debug.Log("Setting destination point for: " + owner.name + " to enterence point at: {" + ShopEnterancePoint + "}");
        owner.NavAgent.SetDestination(ShopEnterancePoint);
        //Debug.Log("Distance from Enterence point: " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination));

        //Debug.Log("AI Destination set to: Shop Enterance {" + ShopEnterancePoint + "}");
        //Debug.Log("Distance to AI Destination Enterance: " + AIShopper.navAgent.remainingDistance); 
    }
    public void Execute()
    {
        //owner.NavAgent.SetDestination(ShopEnterancePoint);
        //Debug.Log("Distance from Enterence point: " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination));

    }
    //public bool stateComplete;
    public bool stateComplete()
    {
        //Debug.Log("Checking State Complete for: StateEntering");
        //if (AIShopper.navAgent.remainingDistance <= 0.5f) <== always returning 0 for some reason
        if (Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination) < 0.5f + owner.NavAgent.stoppingDistance + owner.GetComponent<Collider>().bounds.size.x)
        {
            //Debug.Log(owner.name + " has Completed state: stateEntering. With Distance of: " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination) + " to point: " + owner.NavAgent.destination);
            return true;
        }
        else return false;
    }

    public void onExit()
    {
        //if (stateComplete()) Debug.Log("Exited Entering State");
        //Debug.Log("Shop enterence has been aborted!");
    }
}
