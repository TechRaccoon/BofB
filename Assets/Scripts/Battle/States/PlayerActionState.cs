using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionState : BattleState
{
    private BattleActor player;
    private ActionCommandBase selectedMove;

    
    public PlayerActionState(BattleActor actor) {
        player = actor;
    }

    public override void Enter()
    {
        // 1. Show action menu with player's moves
        UIManager.Instance.ShowActionMenu(player.AvailableMoves);

        // 2. Initialize selection
        UIManager.Instance.SelectFirstMoveButton();
    }

    // Update is called once per frame
    public override void Update()
    {
        // 3. Handle navigation/selection
        if (Input.GetKeyDown(KeyCode.DownArrow))
            UIManager.Instance.SelectNextMove();

        if (Input.GetKeyDown(KeyCode.Space))
            OnMoveSelected(UIManager.Instance.GetSelectedMove());
    }

    private void OnMoveSelected(ActionCommandBase move)
    {
        // 4. Store selection and transition
        selectedMove = move;
        BattleManager.Instance.StateStack.PopState(); // Remove this state
        //BattleManager.Instance.StateStack.PushState(new TargetSelectionState(selectedMove));
    }

    public override void Exit()
    {
        // 5. Clean up UI
        UIManager.Instance.HideActionMenu();
    }
}
