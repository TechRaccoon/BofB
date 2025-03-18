using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TurnStartState : BattleState
{
    private List<BattleActor> _turnOrder;
    private int _currentActorIndex;

    //variable to hold the transforms of the parties positions



    public override void Enter()
    {
        //instanciate the actors objects (player + enemies) in the right place

        //Shuffle the turn order on AllActors List
        _turnOrder = BattleManager.Instance.AllActors.OrderBy(i => Guid.NewGuid()).ToList();

        _currentActorIndex = 0;

        // Start the first actor's turn (player or enemy).
        PushNextActor();
    }

    public override void Update()
    {
        // Called when returning to this state after an actor’s turn
        PushNextActor();
    }

    public override void Exit()
    {
        // No cleanup needed here for this state.
    }

    private void PushNextActor()
    {
        // Check if there are more actors in the turn order
        if (_currentActorIndex < _turnOrder.Count)
        {
            BattleActor nextActor = _turnOrder[_currentActorIndex];
            _currentActorIndex++;

            // Push actor's turn state onto the stack
            BattleManager.Instance.StateStack.PushState(
                new ActorTurnState(nextActor)
            );
        }
        else
        {
            // All actors have acted - check win/lose condition
            //BattleManager.Instance.StateStack.PushState(new CheckWinState());

            // Pop this state from the stack
            BattleManager.Instance.StateStack.PopState();

        }
    }
}