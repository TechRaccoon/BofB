using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorTurnState : BattleState
{
    private BattleActor _actor; // The actor whose turn it is (player/enemy).
    private bool _hasInitialized = false; //to check when re entering state

    public ActorTurnState(BattleActor actor)
    {
        _actor = actor; // Pass in the actor (e.g., Mario or Goomba).
    }

    public override void Enter()
    {
        if (!_hasInitialized)
        {
            // Push child state (PlayerActionState/EnemyActionState)
            if (_actor.CompareTag("Player"))
                BattleManager.Instance.StateStack.PushState(new PlayerActionState(_actor));
            else
                BattleManager.Instance.StateStack.PushState(new EnemyActionState(_actor));

            _hasInitialized = true;
        }
        else {
            BattleManager.Instance.StateStack.PopState();
        }
    }


    public override void Exit()
    {
        // Reset for reuse
        _hasInitialized = false;
    }
}
