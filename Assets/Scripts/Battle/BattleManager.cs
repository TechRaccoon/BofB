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

    // Stores the stack of the battle states (turns) 
    public BattleStack StateStack { get; private set; }

    //Stores both parties
    public PartyManager party;
    public EnemyPartyManager enemyParty;

    // Contains all actors in battle
    public List<IBattleActor> AllActors = new List<IBattleActor>();

    // Arrays to hold the position of the actors
    private Vector3[] playerSide = {
    new Vector3(-1.787f, -0.71f, 4.741f), // POS 1
    new Vector3(-2.53f, -0.72f, 4.385f)    // POS 2
};

    private Vector3[] enemySide = {
        new Vector3{ x = (float)2.01, y = (float)-0.71, z = (float)4.38 }, //POS 1 
        new Vector3{ x = (float)1.35, y = (float)-0.71, z = (float)5.164 } //POS 2

    };


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
        // Find all Parties in the scene (players + enemies)
        try
        {
            party = FindAnyObjectByType<PartyManager>();
            enemyParty = FindAnyObjectByType<EnemyPartyManager>();
        }
        catch (Exception e)
        {
            Debug.Log("Party not found: " + e);
        }

        //place actors in the scene
        //SetActors();
        SetPlayerParty();

        // Starts the first element on the stack
        //StateStack.PushState(new TurnStartState());

    }

    // Propagate Update() to the top state
    void Update()
    {
        
        StateStack.Update();
    }


    //instanciate the actors objects (player + enemies) in the right place
    void SetActors() {
        // Spawn players (using PartyManager's instances)
        foreach (var character in PartyManager.Instance.party)
        {
            //GameObject playerPrefab = Instantiate(BattleActor, battlePosition);
            //playerPrefab.GetComponent<BattleActor>().Initialize(character);
        }

        // Spawn enemies (using EnemyPartyManager's instances)
        foreach (var enemy in EnemyPartyManager.Instance.party)
        {
            //GameObject enemyPrefab = Instantiate(enemyPrefabTemplate, battlePosition);
            //enemyPrefab.GetComponent<BattleActor>().Initialize(enemy);
        }
    }

    private void SetPlayerParty()
    {
        for(int i = 0; i < party.party.Count -1; i++)
        {
            Debug.Log("now the player");
            GameObject playerPrefab = Instantiate(ActorPrefab, playerSide[i], Quaternion.identity);
            Debug.Log($"Spawning at: {playerSide[i]}");

            playerPrefab.GetComponentInChildren<ActorSetUp>().Initialize(party.party[i]);
        }

    }
}
