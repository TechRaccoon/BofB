using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelectionState : BattleState
{
    private MoveBase selectedMove;

    public TargetSelectionState(MoveBase selectedMove)
    {
        this.selectedMove = selectedMove;
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
