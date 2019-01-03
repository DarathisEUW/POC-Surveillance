using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMunching : IState
{

    AIEntity owner;
    public StateMunching(AIEntity owner) { this.owner = owner; }
    //AIShopperScript AIShopper;

    List<Transform> leavingPoints;
    Vector3 leavingPoint;

    ParticleSystem MunchingParticleSystem;// = owner; 

    //slow walk carrying heavy/lots of stuff
    public float munchingTimer;
    float munchingTime;
    float munchDuration = 60;

    public int munchPoints;

    int munchedPoints;

    GameplayScript gameplay = GameObject.Find("GUI Canvas").GetComponent<GameplayScript>();
    levelGameplay  level = GameObject.Find("GUI Canvas").GetComponent<levelGameplay>();

    public void onEnter()
    {
        if (owner.GetComponent<AIMuncherScript>())
        {
            munchingTime = owner.GetComponent<AIMuncherScript>().munchingTime;
            munchPoints = owner.GetComponent<AIMuncherScript>().munchPoints;

            gameplay = GameObject.Find("GUI Canvas").GetComponent<GameplayScript>();
            level = GameObject.Find("GUI Canvas").GetComponent<levelGameplay>();
        }

        //Shopper did something naughty, is a criminal!
        owner.activeCrime = true;

        owner.NavAgent.SetDestination(owner.transform.position);
        //owner.transform.rot

        MunchingParticleSystem = owner.transform.GetChild(0).GetComponent<ParticleSystem>();
        //MunchingParticleSystem = (float)munchDuration;
        MunchingParticleSystem.Play();

    }
    public void Execute()
    {
        munchingTimer += Time.deltaTime;

        if (munchingTimer >= munchingTime)
        {
            munchingTimer = 0;

            if (gameplay)
            {
                int playerscore = gameplay.getScore();
                gameplay.setScore(playerscore -= munchPoints);
            }

            munchedPoints += munchPoints;
        }
    }


    //public bool stateComplete;
    public bool stateComplete()
    {

        if (munchedPoints >= munchDuration) {
            owner.activeCrime = false;
            return true;
        }
        return false;
    }
    public void onExit()
    {
        MunchingParticleSystem.Stop();

        if (owner != null)
        {
            if (stateComplete()) Debug.Log("Muncher has consumed its fill and will now slowly leave the store");
            else Debug.Log(owner.gameObject + " - Exiting mumching State aborted early (before despawn)!");
        }
        else Debug.Log("Aborted before AI Shopper definition!");
        //else Debug.Log("Shopper off screen, left store, now despawning because hopefully out of view");
    }
}
