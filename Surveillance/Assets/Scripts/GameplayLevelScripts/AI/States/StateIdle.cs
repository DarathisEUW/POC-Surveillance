using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IState {

    AIEntity owner;
    float IdleTimer;
    //float timer;

    public StateIdle(AIEntity owner) { this.owner = owner;}
    //AIShopperScript AIShopper;

    public void onEnter()
    {
        //AIShopper = owner.GetComponent<AIShopperScript>();

        //Debug.Log("Standing still playing with self for: " + owner.idleWaitTimer + " seconds");
        owner.NavAgent.SetDestination(owner.NavAgent.transform.position);
    }
    public void Execute()
    {
        IdleTimer += Time.deltaTime;
        owner.idleWaitTimer = IdleTimer;
        //Debug.Log("Idle action. Timer: " + IdleTimer + " / " + owner.itemWaitTime);
    }
    //public bool stateComplete;
    public bool stateComplete()
    {
        if (IdleTimer >= owner.itemWaitTime)
        {
            //Debug.Log("Idle Wait time is up.");
            return true;
        }
        return false;
    }
    public void onExit()
    {
        //if (owner != null)
        //{
        //    if (stateComplete()) Debug.Log("Exited Idle State");
        //    else Debug.Log("Idle Timer has been aborted early!");
        //}
        //else Debug.Log("Aborted before AI Shopper definition!");
    }
}
