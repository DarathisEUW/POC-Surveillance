using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCarryAway : IState
{

    AIEntity owner;
    public StateCarryAway(AIEntity owner) { this.owner = owner; }
    //AIShopperScript AIShopper;

    List<Transform> leavingPoints;
    Vector3 leavingPoint;

    //slow walk carrying heavy/lots of stuff
    public float fleeMod = 1.0f;

    

    public void onEnter()
    {
        //Debug.Log("Entering CarryAway State");
        //AIShopper = owner.GetComponent<AIShopperScript>();
        //leavingPoints = new List<Transform>();
        //Transform ObjectivePoints = GameObject.Find("ObjectivePoints").transform;
        //
        ////Debug.Log("Searching for Despawn Points");
        //foreach (Transform ObjPoint in ObjectivePoints)
        //{
        //    if (ObjPoint.name == "Despawn") leavingPoints.Add(ObjPoint);
        //}
        ////Debug.Log("Despawn Points: " + leavingPoints.Count);
        //
        //int randomLeave = Random.Range(0, leavingPoints.Count);
        //leavingPoint = leavingPoints[randomLeave].position;
        //Debug.Log("Leaving point: " + randomLeave + " at position: " + leavingPoints[randomLeave].position);

        leavingPoint = AIUtility.getRandom("Despawn").transform.position;
        if (owner.NavAgent.isOnNavMesh) owner.NavAgent.SetDestination(leavingPoint);

        //Debug.Log("Leaving shop, distance to exit = " + Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination));
        //Debug.Log("Set Leaving Destination");

        float fleeSpeed = owner.NavAgent.speed * fleeMod;
        owner.NavAgent.speed = fleeSpeed;
        //Debug.Log("Set Move Speed to: " + fleeSpeed);

        //makes carry thiefs carried items active (visible and hitbox) 
        if (owner.GetComponent<AICarrierScript>()) {
            //real owner
            AICarrierScript realOwner = owner.GetComponent<AICarrierScript>();
            realOwner.CarriedItem.SetActive(true);// = true;
        }
        

        //Shopper did something naughty, is a criminal!
        owner.activeCrime = true;
    }
    public void Execute()
    {
        //fleeing
    }
    //public bool stateComplete;
    public bool stateComplete()
    {

        if (Vector3.Distance(owner.NavAgent.transform.position, owner.NavAgent.destination) < 0.5f + owner.NavAgent.stoppingDistance + owner.GetComponent<Collider>().bounds.size.x)
        {
            //Debug.Log("Carried Item to Exit: " + owner.gameObject);
            //Debug.Log(owner.gameObject + " - now despawning!");

            GameplayScript gameplay = GameObject.Find("GUI Canvas").GetComponent<GameplayScript>();
            levelGameplay level = GameObject.Find("GUI Canvas").GetComponent<levelGameplay>();
            if (gameplay)
            {
                int playerscore = gameplay.getScore();
                gameplay.setScore(playerscore -= owner.pointsWorth * 2);
            }
            if (level) level.levelGameplayPoints(false, owner.pointsWorth);

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
        //    else Debug.Log(owner.gameObject + " - Exiting CarryAway State aborted early (before despawn)!");
        //}
        //else Debug.Log("Aborted before AI Shopper definition!");
        //else Debug.Log("Shopper off screen, left store, now despawning because hopefully out of view");
    }
}
