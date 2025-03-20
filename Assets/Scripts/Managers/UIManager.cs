using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject actionMenuPanel;    // Action selection menu
    public GameObject targetPanel;        // Target selection panel
    public RectTransform moveButtonsParent; // Container for move buttons
    public Transform commandUIContainer;  // Parent for action command UI


    [Header("Prefabs")]
    public GameObject moveButtonPrefab;   // Move selection button prefab
    public GameObject defaultCommandPrompt; // Default "Press SPACE" prompt

    [Header("Navigation")]
    public Color selectedColor = Color.yellow;
    public Color normalColor = Color.white;
    public float navCooldown = 0.2f;

    private List<Button> actionButtons = new List<Button>();
    private int selectedIndex = 0;
    private float lastNavTime;
    private GameObject currentCommandUI;  // Currently active command UI


    internal void SelectFirstMoveButton()
    {
        throw new NotImplementedException();
    }

    internal void SelectNextMove()
    {
        throw new NotImplementedException();
    }

    internal MoveBase GetSelectedMove()
    {
        throw new NotImplementedException();
    }

    
    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        if (actionMenuPanel.activeSelf)
        {
            HandleMenuNavigation();
        }

        if (currentCommandUI != null)
        {
            HandleActionCommandInput();
        }
    }

    // Show action menu with player's available moves
    public void ShowActionMenu(List<MoveBase> moves)
    {
        actionMenuPanel.SetActive(true);
        selectedIndex = 0;
        ClearMoveButtons();
        actionButtons.Clear();

        foreach (MoveBase move in moves)
        {
            GameObject buttonObj = Instantiate(moveButtonPrefab, moveButtonsParent);
            MoveButton moveButton = buttonObj.GetComponent<MoveButton>();
            moveButton.Initialize(move);
            actionButtons.Add(buttonObj.GetComponent<Button>());
        }

        UpdateButtonColors();
    }

    void HandleMenuNavigation()
    {
        float vertical = Input.GetAxisRaw("Vertical");

        if (Time.time - lastNavTime > navCooldown && Mathf.Abs(vertical) > 0.5f)
        {
            selectedIndex = Mathf.Clamp(
                selectedIndex + (vertical > 0 ? -1 : 1),
                0,
                actionButtons.Count - 1
            );
            UpdateButtonColors();
            lastNavTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            actionButtons[selectedIndex].onClick.Invoke();
        }
    }

    void UpdateButtonColors()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].image.color = i == selectedIndex ? selectedColor : normalColor;
        }
    }


    public void HideActionMenu() => actionMenuPanel.SetActive(false);

    // Show target selection UI
    public void ShowTargetPanel() => targetPanel.SetActive(true);
    public void HideTargetPanel() => targetPanel.SetActive(false);

    // Show specific action command UI
    public void ShowActionCommand(ActionCommandBase command)
    {
        ClearCurrentCommandUI();
        StartCoroutine(ActionCommandRoutine(command));
    }

    IEnumerator ActionCommandRoutine(ActionCommandBase command)
    {
        // Show UI
        if (command.uiPrefab != null)
        {
            currentCommandUI = Instantiate(command.uiPrefab, commandUIContainer);
        }
        else
        {
            currentCommandUI = Instantiate(defaultCommandPrompt, commandUIContainer);
            Text promptText = currentCommandUI.GetComponentInChildren<Text>();
            if (promptText != null) promptText.text = command.defaultPrompt;
        }

        // Wait for input or timeout
        float timer = 0;
        bool success = false;

        while (timer < command.duration)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                success = true;
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        // Notify battle system
        if (success)
        {
            BattleManager.Instance.OnActionCommandSuccess();
        }
        else
        {
            BattleManager.Instance.OnActionCommandFail();
        }

        ClearCurrentCommandUI();
    }

    void HandleActionCommandInput()
    {
        // Additional input handling can be added here
        // if using more complex command types
    }

    public void HideActionCommand()
    {
        ClearCurrentCommandUI();
    }

    // Clear existing move buttons
    private void ClearMoveButtons()
    {
        foreach (Transform child in moveButtonsParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void ClearCurrentCommandUI()
    {
        if (currentCommandUI != null)
        {
            Destroy(currentCommandUI);
            currentCommandUI = null;
        }
    }

    // Get current command UI component
    public T GetCommandUI<T>() where T : MonoBehaviour
    {
        return currentCommandUI?.GetComponent<T>();
    }
}