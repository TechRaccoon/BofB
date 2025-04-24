using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStack 
{
    // Stores the stack of active states
    private Stack<BattleState> stack = new Stack<BattleState>();

    // Adds a new state to the top of the stack and runs it.
    public void PushState(BattleState state) {

        // Add the new state to the stack.
        stack.Push(state);

        // Initialize the new state by calling Enter().
        state.Enter();
    }

    // Removes the top state from the stack.
    public void PopState() {
        if (stack.Count == 0) return;

        // Clean up the old state by calling Exit().
        stack.Peek().Exit();

        // Get the top state and remove it.
        stack.Pop();
    }

    // Updates the current top state (for timers or input checks).
    public void Update()
    {

        if (stack.Count > 0) {

            // Delegate Update() to the active state.
            stack.Peek().Update();
        }
    }
}
