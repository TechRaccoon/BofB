using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState
{
    // Called when the state is pushed onto the stack.
    public abstract void Enter();

    // Called when the state is popped from the stack.
    public abstract void Exit();

    // Optional: Called every frame while the state is active.
    public virtual void Update() { }
}
