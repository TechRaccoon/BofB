using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorTurnState : BattleState
{
    private BattleActor _actor; // The actor whose turn it is (player/enemy).

    public ActorTurnState(BattleActor actor)
    {
        _actor = actor; // Pass in the actor (e.g., Mario or Goomba).
    }

    public override void Enter()
    {
        // Decide whether to show the player menu or run enemy AI.
        if (_actor.CompareTag("Player"))
        {
            BattleManager.Instance.StateStack.PushState(
                new PlayerActionState(_actor)
            );
        }
        else
        {
            BattleManager.Instance.StateStack.PushState(
                new EnemyActionState(_actor)
            );
        }
    }

    public override void Exit()
    {
        // No cleanup needed here.
    }
}
