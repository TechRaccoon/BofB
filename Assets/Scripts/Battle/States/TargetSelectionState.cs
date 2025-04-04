using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelectionState : BattleState
{
    private ActionCommandBase selectedMove;

    public TargetSelectionState(ActionCommandBase selectedMove)
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
