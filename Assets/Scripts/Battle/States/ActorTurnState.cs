using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorTurnState : BattleState
{
    private IBattleActor _actor; // The actor whose turn it is (player/enemy).

    private bool _hasInitialized = false; //to check when re entering state

    public ActorTurnState(IBattleActor actor)
    {
        _actor = actor; // Pass in the actor 
    }

    public override void Enter()
    {
        //CastCharacter();
        Debug.Log("In ActorTurnState");
        BattleManager.Instance.StateStack.PopState();
    }


    public override void Exit()
    {
        // Reset for reuse
        Debug.Log("Exiting ActorTurnState");
        _hasInitialized = false;
    }

    // Determines if _actor is player or enemy
    // Cast the actor into their base instance type
    // Calls the new State based on type
    public void CastCharacter()
    {
        if (!_hasInitialized)
        {
            if (_actor is CharacterInstance character)
                BattleManager.Instance.StateStack.PushState(new PlayerActionState(character));

            else if (_actor is EnemyInstance enemy)
                BattleManager.Instance.StateStack.PushState(new EnemyActionState(enemy));

            _hasInitialized = true;
        }
        else
        {
            BattleManager.Instance.StateStack.PopState();
        }
    }
}
