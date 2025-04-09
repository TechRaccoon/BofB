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

    //

    // Contains all actors in battle
    public List<IBattleActor> AllActors = new List<IBattleActor>();

    //variable to hold the position of the actors
    public Vector3[] playerSide = { new Vector3 { x = (float)-1.84, y = 0, z = 0 } };
    public Vector3[] enemySide = { new Vector3{ x = (float)1.96, y = 0, z = 0 } };

    public Transform playerSide1;
    public Transform enemySide1;

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

    internal void SelectMove()//move should be here
    {
        throw new NotImplementedException();
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
        //Instantiate(AllActors[0],playerSide1);

        //Instantiate(AllActors[1], enemySide1);
    }
}
