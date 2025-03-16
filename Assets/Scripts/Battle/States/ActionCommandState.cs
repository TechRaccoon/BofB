using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommandState : BattleState
{
    private MoveBase _move;       // The move being executed
    private BattleActor _user;    // Who is using the move (e.g., player)
    private BattleActor _target;  // The target (e.g., enemy)
    private Coroutine _inputRoutine;

    public ActionCommandState(MoveBase move, BattleActor user, BattleActor target)
    {
        _move = move;
        _user = user;
        _target = target;
    }

    public override void Enter()
    {
        // Show UI prompt (e.g., "Press SPACE NOW!")
        UIManager.Instance.ShowActionCommand(ActionCommandBase command);

        // Start listening for input
        _inputRoutine = BattleManager.Instance.StartCoroutine(WaitForInput());
    }

    IEnumerator WaitForInput()
    {
        bool success = false;
        float timer = 0f;
        float inputWindow = 1.5f; // Time the player has to press the button

        // Wait for input or timeout
        while (timer < inputWindow)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                success = true;
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        // Notify the move of success/failure
        if (success)
        {
            _move.OnActionCommandSuccess(_user, _target);
        }
        else
        {
            _move.OnActionCommandFail(_user, _target);
        }

        // Pop this state to return to the previous logic (e.g., resolving the move)
        BattleManager.Instance.StateStack.PopState();
    }

    public override void Exit()
    {
        UIManager.Instance.HideActionCommand();
        if (_inputRoutine != null)
        {
            BattleManager.Instance.StopCoroutine(_inputRoutine);
        }
    }
}
