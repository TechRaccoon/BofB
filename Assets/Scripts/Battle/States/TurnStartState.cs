using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TurnStartState : BattleState
{
    private List<IBattleActor> _turnOrder;
    private int _currentActorIndex;


    public override void Enter()
    {
        Debug.Log("In TurnStart State");
        _currentActorIndex = 0;

        //Shuffle the turn order on AllActors List
        _turnOrder = ShuffledList(BattleManager.Instance.AllActors);

        // Start the first actor's turn 
        PushNextActor();
    }

    public override void Update()
    {
        if (BattleManager.Instance.isBattleOver)
        {
            // Push Finish State 
        }
        else
        {
            // Called when returning to this state after an actor’s turn
            PushNextActor();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting TurnStart State");
        // No cleanup needed here for this state.
    }

    private void PushNextActor()
    {
        // Check if there are more actors in the turn order
        if (_currentActorIndex < _turnOrder.Count)
        {
            IBattleActor nextActor = _turnOrder[_currentActorIndex];
            _currentActorIndex++;

            // Push actor's turn state onto the stack
            BattleManager.Instance.StateStack.PushState(new ActorTurnState(nextActor));
        }
        else
        {
            // Round is over, reshuffle List
            _turnOrder = ShuffledList(BattleManager.Instance.AllActors);

            _currentActorIndex = 0;

        }
    }

    // Fisher-Yates Shuffle Algorithm
    public List<IBattleActor> ShuffledList(List<IBattleActor> list)
    {
        for (int i = list.Count -1; i >= 0; i--)
        {
            int j = UnityEngine.Random.Range(0,i + 1); //non inclusive
            IBattleActor temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }
}