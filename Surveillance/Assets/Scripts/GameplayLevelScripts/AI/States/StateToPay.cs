using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class StateToPay : IState
{
    AIEntity owner;

    public StateToPay(AIEntity owner) { this.owner = owner; }
    //AIShopperScript AIShopper;

    Vector3 PaymentPoint;


    public void onEnter()
    {
        //Debug.Log("Im entering ToPay State!");

        PaymentPoint = GameObject.Find("CashierDeskPay").transform.position;
        //AIShopper = owner.GetComponent<AIShopperScript>();


        //Debug.Log("Setting destination point for: " + owner.name + " to enterence point at: {" + PaymentPoint + "}");
        owner.NavAgent.SetDestination(PaymentPoint);
        //Debug.Log("Distance from Payment point: " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination));
        //Debug.Log("AI Destination set to: Shop Enterance {" + ShopEnterancePoint + "}");
        //Debug.Log("Distance to AI Destination Enterance: " + AIShopper.navAgent.remainingDistance); 
    }
    public void Execute()
    {
        //AIShopper.navAgent.SetDestination(PaymentPoint);
        //Debug.Log("Distance from Payment point: " + Vector3.Distance(AIShopper.navAgent.transform.position, AIShopper.navAgent.destination));
    }
    //public bool stateComplete;
    public bool stateComplete()
    {
        //Debug.Log("Checking State Complete for: stateToPay");

        if (Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination) < 0.5f + owner.NavAgent.stoppingDistance + owner.GetComponent<Collider>().bounds.size.x)
        {
            //Debug.Log(owner.name + " has Completed state: toPay. With Distance of: " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination) + " to point: " + owner.NavAgent.destination);
            return true;
        }
        else return false;
    }

    public void onExit()
    {
        //if (stateComplete()) Debug.Log("Exited toPayment State");
        //else Debug.Log("ToPayment has been aborted!");
    }
}
