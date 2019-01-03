using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCaptured : IState
{

    AIEntity owner;
    public StateCaptured(AIEntity owner) { this.owner = owner; }

    bool animationComplete;
    int animationTime;
    float animationTimer;

    public void onEnter()
    {
        //Debug.Log("Entering Captured State");
        if(owner.NavAgent.isOnNavMesh)owner.NavAgent.SetDestination(owner.gameObject.transform.position);

        animationComplete = false;
        animationTime = 3;
        animationTimer = 0f;
    }
    public void Execute()
    {
        //Debug.Log("Playing temp Captured Animation");

        animationTimer += Time.deltaTime;
        //if(animationTimer >= animationTime) 
    }
    //public bool stateComplete;
    public bool stateComplete()
    {
        //Debug.Log("Checking State Complete for: StateLeaving");

        //if (AIShopper.navAgent.remainingDistance <= 0.5f)
        if (animationTimer >= animationTime)
        {
            //Debug.Log("Capture Complete: " + owner.gameObject);
            //Debug.Log(owner.gameObject + " - now captured!");
            //GameObject.Destroy(owner.gameObject);
            return true;
        }
        return false;
    }
    public void onExit()
    {
        //if (owner != null)
        //{
        //    if (stateComplete()) 
        //    {
        //        Debug.Log("Shopper captured and animation complete, now despawning!");
        //        GameObject.Destroy(owner.gameObject);
        //    }
        //    else Debug.Log(owner.gameObject + " - Exiting StateLeaving aborted early (before despawn)!");
        //}
        //else Debug.Log("Aborted before AI Shopper definition!");
        //else Debug.Log("Shopper off screen, left store, now despawning because hopefully out of view");
    }
}
