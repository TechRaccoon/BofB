using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionState : BattleState
{
    private BattleActor player;

    public PlayerActionState(BattleActor actor) {
        player = actor;
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
