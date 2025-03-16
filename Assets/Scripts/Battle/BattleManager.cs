using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //Singleton declartion
    public static BattleManager Instance { get; private set; }

    // Stores the stack of the battle states (turns) 
    public BattleStack StateStack { get; private set; }

    // Contains all actors in battle
    public List<BattleActor> AllActors = new List<BattleActor>();

    void Wake()
    {
        if (Instance == null) Instance = this;
        StateStack = new BattleStack();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Find all BattleActors in the scene (players + enemies)
        AllActors.AddRange(FindObjectsOfType<BattleActor>());

        //Shuffle the turn order
        //StateStack.PushState(new TurnStartState());

    }

    // Update is called once per frame
    void Update()
    {
        StateStack.Update();
    }

    private void Shuffle()
    {

    }
}
