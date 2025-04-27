using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Singleton declartion
    public static BattleManager Instance { get; private set; }

    // Stores the reference to the battleActor prefab
    [SerializeField]public GameObject ActorPrefab;

    // Stores the stack of the battle states
    public BattleStack StateStack { get; private set; }

    //Stores both parties
    public PartyManager party;
    public EnemyPartyManager enemyParty;

    // Contains all actors in battle
    public List<IBattleActor> AllActors = new List<IBattleActor>();

    // 
    public bool isBattleOver;

    // Upon Intanciation checks if theres no other Battlemanger Insance
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        StateStack = new BattleStack(); // Instanciates a new Stack of BattleStates 
    }


    void Start()
    {
        isBattleOver = false; // global confirmation

        SetUpParties(); // Find all Parties in the scene (players + enemies)

        ListedActors(); // Initializes the AllActors list

        StateStack.PushState(new SetUpState()); //Push the SetUp Satate

    }

    
    void Update() 
    {
        StateStack.Update(); // Propagates Update() to the top battle state
        //Debug.Log("BattleManager UPDATE!");
    }


    private void SetUpParties()
    { // Find all Parties in the scene (players + enemies)
        try
        {
            party = FindAnyObjectByType<PartyManager>();
            enemyParty = FindAnyObjectByType<EnemyPartyManager>();
        }
        catch (Exception e)
        {
            Debug.Log("Party not found: " + e);
        }
    }


    private void ListedActors()
    {
        foreach(IBattleActor player in party.party)
        {
            AllActors.Add(player);
        }

        foreach (IBattleActor enemy in enemyParty.party)
        {
            AllActors.Add(enemy);
        }
        //Debug.Log(AllActors.Count);
    }

    ///////////////// E X P E R I M E N T S ////////////////////////////




}
