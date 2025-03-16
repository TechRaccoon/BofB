using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject actionMenuPanel;    // Parent panel for the action menu
    public GameObject targetPanel;        // Panel for selecting targets
    public Text actionCommandText;        // Displays "Press SPACE NOW!"
    public RectTransform moveButtonsParent; // Container for move buttons

    [Header("Prefabs")]
    public GameObject moveButtonPrefab;   // Prefab for move selection buttons

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Show/hide the action menu (e.g., Attack/Item/Badge)
    public void ShowActionMenu(List<MoveBase> moves)
    {
        actionMenuPanel.SetActive(true);
        ClearMoveButtons();

        // Populate the menu with buttons for each move
        foreach (MoveBase move in moves)
        {
            GameObject buttonObj = Instantiate(moveButtonPrefab, moveButtonsParent);
            //MoveButton moveButton = buttonObj.GetComponent<MoveButton>();
            //moveButton.Initialize(move);
        }
    }

    public void HideActionMenu()
    {
        actionMenuPanel.SetActive(false);
    }

    // Show/hide target selection UI
    public void ShowTargetPanel()
    {
        targetPanel.SetActive(true);
    }

    public void HideTargetPanel()
    {
        targetPanel.SetActive(false);
    }

    // Action command UI
    public void ShowActionCommandPrompt()
    {
        actionCommandText.gameObject.SetActive(true);
        actionCommandText.text = "Press SPACE NOW!";
    }

    public void HideActionCommandPrompt()
    {
        actionCommandText.gameObject.SetActive(false);
    }

    // Clear existing move buttons
    private void ClearMoveButtons()
    {
        foreach (Transform child in moveButtonsParent)
        {
            Destroy(child.gameObject);
        }
    }
}
