using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Singleton declartion
    public static BattleManager Instance { get; private set; }

    // Stores the stack of the battle states (turns) 
    public BattleStack StateStack { get; private set; }

    // Contains all actors in battle
    public List<BattleActor> AllActors = new List<BattleActor>();

    //variable to hold the transform of the battle positions

    internal void SelectMove(MoveBase move)
    {
        throw new NotImplementedException();
    }

    // Upon Intanciation checks is theres no other Battlemanger Insance
    // Instanciates a new Stack of BattleStates 
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        StateStack = new BattleStack();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Find all BattleActors in the scene (players + enemies)
        AllActors.AddRange(FindObjectsOfType<BattleActor>());

        //place actors in the scene
        SetActors();

        // Starts the first element on the stack
        StateStack.PushState(new TurnStartState());

    }

    // Propagate Update() to the top state
    void Update()
    {
        
        StateStack.Update();
    }

    internal void OnActionCommandSuccess()
    {
        throw new NotImplementedException();
    }

    internal void OnActionCommandFail()
    {
        throw new NotImplementedException();
    }

    //instanciate the actors objects (player + enemies) in the right place
    void SetActors() {
        //to be implemented
    }
}
