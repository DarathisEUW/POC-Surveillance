using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateToShelfPoint : IState
{
    AIEntity owner;
    public StateToShelfPoint(AIEntity owner) { this.owner = owner; }
    //AIShopperScript AIShopper;

    //Transform Shelves;
    //List<Transform> ShelfPoints = new List<Transform>();
    //
    //
    public Vector3 ShopShelfTargetPoint;
    public Transform randomShelf;

    public void onEnter()
    {
        //Shelves = GameObject.Find("ShelfPoints").transform;
        ////AIShopper = owner.GetComponent<AIShopperScript>();
        //
        ////AIShopper.reachedShopPoint = false;
        //
        //
        //foreach (Transform point in Shelves) ShelfPoints.Add(point);
        //
        //desiredShelfPoint = Random.Range(0, ShelfPoints.Count - 1);
        //ShopShelfTargetPoint = ShelfPoints[desiredShelfPoint].gameObject.transform.position;
        ShopShelfTargetPoint = AIUtility.getRandom("ShelfPoint").transform.position;

        owner.NavAgent.SetDestination(ShopShelfTargetPoint);
        //Debug.Log("Traveling to shelf point: {" + ShopShelfTargetPoint + "} Distance from: " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination));
    }
    public void Execute()
    {
        //owner.NavAgent.SetDestination(ShopShelfTargetPoint);
        //Debug.Log("Traveling to shelf point: {" + ShopShelfTargetPoint + "} Distance from: " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination));
        //if (AIShopper.navAgent.remainingDistance <= 0.5f) AIShopper.desiredItemsCount++; AIShopper.StateMachine.ChangeState(new StateIdle(owner, 5.0f)); Debug.Log("Waiting before picking up next item!");

    }
    //public bool stateComplete;
    public bool stateComplete()
    {
        //if (AIShopper.navAgent.remainingDistance <= 0.5f)
        if (Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination) < 0.5f + owner.NavAgent.stoppingDistance + owner.GetComponent<Collider>().bounds.size.x)
            {
            owner.desiredItemCount += 1;
            //Debug.Log(owner.name + " has Completed state: StateToShelfPoint. With Distance of: " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination) + " to point: " + owner.NavAgent.destination);
            return true;
        }
        else return false;
    }
    public void onExit()
    {
        if(owner != null)
        {
            //if (stateComplete()) Debug.Log("Shelf point reached, Exiting State");
            //else Debug.Log("Aborting leaving before shelf could be reached");
            //Debug.Log("Shelf Either Reached, or Aborted Early. Too scared to do a complete check because it triggers the counter to ++ and Im too lazy to create a workaround!");
        }
        //else Debug.Log("Aborted before AI Shopper definition!");
    }
}
