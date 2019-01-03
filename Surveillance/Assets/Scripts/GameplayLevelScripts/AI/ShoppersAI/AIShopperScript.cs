using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

//Basic AI Shopper who Spawns, enters shop, picks up Items, pays for items, and leaves
public class AIShopperScript : AIEntity
{

    //AIFiniteStateMachine StateMachine = new AIFiniteStateMachine();
    //public NavMeshAgent NavAgent;

    // Spawning Shopper in to level at spawn points
    List<Transform> spawnPoints;
    Vector3 spawnPoint;

    //basic shopper vars for 
    //public int desiredItems;
    //public int desiredItemCount;
    //public float itemWaitTime;
    //public float idleWaitTimer;

        

    public override void Start()
    {
        StateMachine = new AIFiniteStateMachine();
        StateMachine.StateStack = new Stack<IState>();

        NavAgent = gameObject.GetComponent<NavMeshAgent>();

        pointsWorth = 0;

        Spawn();
        StackStates();
    }

	// Update is called once per frame
	public override void Update ()
    {
        // murgency oshit we don't have states lets make sure he leaves store before disappearing for reasons
        //if (StateMachine.StateStack.Count < 1) StateMachine.ChangeState(new StateLeaving(this));

        //Updates for each active state
        //StateMachine.ChangeState(CurrentState());
        //Debug.Log("State: " + StateMachine.currentState() + " Updating");
        StateMachine.Update();
        if (StateMachine.currentState().stateComplete() || StateMachine.currentState() == null) StateMachine.nextState();
        //if (StateMachine.StateStack.Count < 1) StateMachine.ChangeState(new StateLeaving(this));

	}

    public override void Spawn()
    {
        spawnPoints = new List<Transform>();
        Transform ObjectivePoints = GameObject.Find("ObjectivePoints").transform;

        //Debug.Log("Searching for Spawn Points");
        foreach (Transform ObjPoint in ObjectivePoints)
        {
            if (ObjPoint.name == "Spawn") spawnPoints.Add(ObjPoint);
        }

        int randomSpawn = Random.Range(0, spawnPoints.Count);
        spawnPoint = spawnPoints[randomSpawn].position;
        NavAgent.Warp(spawnPoint);

        Debug.Log("Spawned: [" + gameObject.name + "] at Spawn point [" + randomSpawn + "]");
    }
    public override void StackStates()
    {
        StateMachine.addState(new StateLeaving(this));

        StateMachine.addState(new StateIdle(this));
        StateMachine.addState(new StateToPay(this));

        desiredItems = Random.Range(1, 5);
        itemWaitTime = Random.Range(1, 5);

        for (int i = 0; i < desiredItems; i++)
        {
            StateMachine.addState(new StateIdle(this));
            StateMachine.addState(new StateToShelfPoint(this));
        }

        StateMachine.ChangeState(new StateEntering(this));

        //Debug.Log("Stacked State machine for unit: " + gameObject.name + " Stack: " + StateMachine.StateStack + " of size: " + StateMachine.StateStack.Count);
    }
}
