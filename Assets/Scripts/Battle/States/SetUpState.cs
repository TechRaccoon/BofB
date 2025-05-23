using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetUpState : BattleState
{
    private BattleManager manager;

    const int duo = 2; // Limit for battle party

    private Vector3[] playerSide = { // Arrays to hold the position of the actors
        new Vector3(-1.787f, -0.71f, 4.741f), // POS 1
        new Vector3(-2.53f, -0.72f, 4.385f)   // POS 2
    };
    private Vector3[] enemySide = {
        new Vector3( 2.01f, -0.71f, 4.38f ), // POS 1 
        new Vector3(1.35f, -0.71f, 5.164f)   // POS 2
    };

    public override void Enter()
    {
        Debug.Log("In SetUp State");
        
        manager = BattleManager.Instance; // For readability

        SetUpPlayerParty(); // Set up the player Party

        SetEnemyParty();    //Set up the enemy party

        manager.StateStack.PushState(new TurnStartState());
    }

    public override void Exit()
    {
        // no cleaning so far
        Debug.Log("Exiting SetUp State");
    }

    public void SetUpPlayerParty()
    {
        for (int i = 0; i < duo; i++)
        {
            // Instanciating player prefab on the right location 
            GameObject playerPrefab = UnityEngine.Object.Instantiate(manager.ActorPrefab, playerSide[i], Quaternion.identity);

            // Initialize the prefab with the player data
            playerPrefab.GetComponentInChildren<ActorSetUp>().Initialize(manager.party.party[i]);

            // Instanciate the battle HUD for the player with the player data
            UIManager.Instance.SetUpPlayerHUD(manager.party.party[i]);
        }
    }

    
    private void SetEnemyParty() //Instanciate the enemy party
    {
        for (int i = 0; i < manager.enemyParty.party.Count; i++)
        {
            // Instanciating enemy prefab on the right location
            GameObject enemyPrefab = UnityEngine.Object.Instantiate(manager.ActorPrefab, enemySide[i], Quaternion.identity);

            // Initialize the prefab with the enemy data
            enemyPrefab.GetComponentInChildren<ActorSetUp>().Initialize(manager.enemyParty.party[i]);

            // Instanciate the battle HUD for the player with the player data
            enemyPrefab.GetComponentInChildren<Animator>().Play("IDLE_SW");

        }
    }

    public override void Update()
    {
        Debug.Log("IF THIS PRINTS MEANS THAT UPDATE GEST CALLED AT LEAST ONCE");
    }

}
