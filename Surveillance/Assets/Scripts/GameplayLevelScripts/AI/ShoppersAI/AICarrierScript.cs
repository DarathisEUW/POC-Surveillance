using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


public class AICarrierScript : AIEntity
{
    //AIFiniteStateMachine StateMachine = new AIFiniteStateMachine();
    //public NavMeshAgent navAgent;


    // Spawning Shopper in to level at spawn points
    List<Transform> spawnPoints;
    Vector3 spawnPoint;

    public GameObject CarriedItem;

    // Use this for initialization
    public override void Start()
    {
        StateMachine = new AIFiniteStateMachine();
        StateMachine.StateStack = new Stack<IState>();

        NavAgent = gameObject.GetComponent<NavMeshAgent>();

        pointsWorth = 15;

        CarriedItem = transform.FindChild("CarriedItem").gameObject;
        CarriedItem.SetActive(false);

        Spawn();
        StackStates();

    }

    // Update is called once per frame
    public override void Update()
    {
        //Debug.Log("State: " + StateMachine.currentState() + " Updating");
        StateMachine.Update();
        if (StateMachine.currentState().stateComplete() || StateMachine.currentState() == null) StateMachine.nextState();
    }

    public override void Spawn()
    {
        //Debug.Log("Spawning AI Unit: " + gameObject.name);
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
        //thief flees from shop with goodies
        StateMachine.addState(new StateCarryAway(this));

        //thief browses for a little bit
        desiredItems = Random.Range(1, 5);
        itemWaitTime = Random.Range(1, 5);

        for (int i = 0; i < desiredItems; i++)
        {
            StateMachine.addState(new StateIdle(this));
            StateMachine.addState(new StateToShelfPoint(this));
        }

        StateMachine.ChangeState(new StateEntering(this));
    }
}
