using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MoveBase : ScriptableObject
{
    [Header("Base Properties")]
    public string Name;
    public int ValorCost;
    public Sprite Icon;
    public enum TargetType { SingleEnemy, AllEnemies, Self }
    public TargetType targetType;
    public ActionCommandBase actionCommand;

    // Called when the move is selected and executed
    public abstract void Execute(BattleActor user, BattleActor target);

    // Called if the action command (e.g., timed press) succeeds
    public virtual void OnActionCommandSuccess(BattleActor user, BattleActor target) { }

    // Called if the action command half succeeds (not fail)
    public virtual void OnActionCommandHalfSuccess(BattleActor user, BattleActor target) { }

    // Called if the action command fails
    public virtual void OnActionCommandFail(BattleActor user, BattleActor target) { }
}
