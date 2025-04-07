using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStack : MonoBehaviour
{
    // Stores the stack of active states
    private Stack<BattleState> stack = new Stack<BattleState>();

    // Adds a new state to the top of the stack and runs it.
    public void PushState(BattleState state) {
        // Pause the current state (if any) by calling Exit().
        if (stack.Count > 0) stack.Peek().Exit();

        // Add the new state to the stack.
        stack.Push(state);

        // Initialize the new state by calling Enter().
        state.Enter();
    }

    // Removes the top state from the stack.
    public void PopState() {
        if(stack.Count > 0)
        {
            // Get the top state and remove it.
            BattleState oldstate = stack.Pop();

            // Clean up the old state by calling Exit().
            oldstate.Exit();
        }

        // Resume the previous state (if any) by calling Enter().
        if (stack.Count > 0) stack.Peek().Enter();
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
