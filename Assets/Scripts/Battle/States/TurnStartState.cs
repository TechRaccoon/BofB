using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStartState : BattleState
{
    public override void Enter()
    {
        // Shuffle the turn order (like Paper Mario's random turn order).
        BattleManager.Instance.AllActors.Sort();

        // Start the first actor's turn (player or enemy).
        BattleManager.Instance.StateStack.PushState(
            new ActorTurnState(BattleManager.Instance.AllActors[0])
        );
    }

    public override void Exit()
    {
        // No cleanup needed here for this simple state.
    }
}
