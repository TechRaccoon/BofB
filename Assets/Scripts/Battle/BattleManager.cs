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
        new Vector3( 2.01f, -0.71f, 4.38f ), //POS 1 
        new Vector3(1.35f, -0.71f, 5.164f)  //POS 2
    };

    const int duo = 2; // Limit for battle party


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
        SetActors();
        

        // Starts the first element on the stack
        //StateStack.PushState(new TurnStartState());
        Debug.Log("");

        StartCoroutine(testHealthChanges());
    }

    // Propagate Update() to the top state
    void Update()
    {
        
        StateStack.Update();
    }


    // Instanciate the actors objects (player + enemies) in the right place
    void SetActors() {
        SetPlayerParty();
        SetEnemyParty();
    }

    // Instanciate the duo battle party
    private void SetPlayerParty()
    {
        for(int i = 0; i < duo; i++)
        {
            // Instanciating player prefab on the right location 
            GameObject playerPrefab = Instantiate(ActorPrefab, playerSide[i], Quaternion.identity);

            // Initialize the prefab with the player data
            playerPrefab.GetComponentInChildren<ActorSetUp>().Initialize(party.party[i]);

            // Instanciate the battle HUD for the player with the player data
            UIManager.Instance.SetUpPlayerHUD(party.party[i]);

        }
    }

    //Instanciate the enemy party
    private void SetEnemyParty()
    {
        for (int i = 0; i < enemyParty.party.Count; i++)
        {
            GameObject enemyPrefab = Instantiate(ActorPrefab, enemySide[i], Quaternion.identity);
            enemyPrefab.GetComponentInChildren<ActorSetUp>().Initialize(enemyParty.party[i]);
            enemyPrefab.GetComponentInChildren<Animator>().Play("IDLE_SW");

        }
    }

    IEnumerator testHealthChanges()
    {
        yield return new WaitForSeconds(3);
        party.party[0].TakeDamage(5);
    }

    

}
