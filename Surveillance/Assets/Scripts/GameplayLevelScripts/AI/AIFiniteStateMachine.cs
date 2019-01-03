using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//unity AI
using UnityEngine.AI;

public interface IState
{
    bool stateComplete();
    void onEnter();
    void Execute();
    void onExit();
}

public class AIFiniteStateMachine : MonoBehaviour
{
    //public IState currentState;

    public Stack<IState> StateStack;// = new Stack<IState>();

    public IState currentState()
    {
        //Debug.Log("Stack: " + StateStack + " size of: " + StateStack.Count);
        //Debug.Log("Peeking (ehehhe) at state stack: " + StateStack + " State: " + StateStack.Peek());
        if (StateStack.Count > 0) return StateStack.Peek();
        else return null;
    }

    public void addState(IState newState)
    {
        //Debug.Log("Adding state to stack: " + newState);
        StateStack.Push(newState);
        //Debug.Log("Added state to stack: " + newState);
    }

    // functions as a manual force of first pos of stack for immediate override of a state with exiting current state and starting new one
    public void ChangeState(IState newState)
    {
        //Debug.Log("State Machine Changing State!");
        if (currentState() != null)
        {
            currentState().onExit();
            //Debug.Log(currentState() + " exited!");
        }
        //else Debug.Log("State Null Cannot Run onExit() method for State.");

        //Debug.Log("Pushing new state to stack: " + newState);
        StateStack.Push(newState);
        //Debug.Log("Pushed new state to stack: " + newState);

        if (currentState() != null)
        {
            currentState().onEnter();
            //Debug.Log("Entered State for: " + currentState());
        }
        //else Debug.Log("State Null Cannot Run onEnter() method for State.");

        //Debug.Log("State successfully changed to " + currentState() + "!");
    }
    public void nextState()
    {
        if (currentState() != null) currentState().onExit(); //Debug.Log("onExit() of: " + currentState());
        StateStack.Pop();
        if (currentState() != null) currentState().onEnter(); //Debug.Log("onEnter() of: " + currentState());
    }
    public void Update()
    {
        //Debug.Log("Updating States!");
        if (currentState() != null) currentState().Execute();
        //Debug.Log("I managed to execute!");
    }
}

//public class TestState : IState
//{
//    AIEntity owner;
//    
//    public TestState(AIEntity owner) { this.owner = owner; }
//
//    //public bool stateComplete;
//    public bool stateComplete()
//    {
//        Debug.Log("State Completed for Test State, initiate popping");
//        return false;
//    }
//
//    public void onEnter()
//    {
//        Debug.Log("Entering Test State!");
//    }
//    public void Execute()
//    {
//        Debug.Log("Executing Test State.");
//    }
//    public void onExit()
//    {
//        Debug.Log("Exiting Test State!");
//    }
//}

//base calss to inherit from
public abstract class AIEntity : MonoBehaviour
{
    public AIFiniteStateMachine StateMachine;// = new AIFiniteStateMachine();
    public NavMeshAgent NavAgent;// = new NavMeshAgent();

    //StateMachine.StateStack = new Stack<IState>();


    //basic shopper vars for timings and shop based activities
    public int desiredItems;
    public int desiredItemCount;
    public float itemWaitTime;
    public float idleWaitTimer;

    public int visiblePoints; // assigned from objectsScanner script

    public bool activeCrime;
    public int pointsWorth;

    public virtual void Start()
    {
        //StateMachine.ChangeState(new TestState(this));
    }
    public virtual void Update()
    {
        //StateMachine.Update();
    }

    public virtual void Spawn()
    {
        //handles spawning of entity into level in correct places
    }
    public virtual void StackStates()
    {
        //stacks the ai states for entity
    }
}
