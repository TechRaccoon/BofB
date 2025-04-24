using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActionCommandBase : ScriptableObject
{
    [Header("General Settings")]
    public float duration = 2f;          // Time window for the command
    public string defaultPrompt = "Press SPACE!"; // Fallback text prompt
    public GameObject uiPrefab;          // Custom UI prefab (optional)

    [Header("Success/Failure Events")]
    public UnityEvent onSuccess;
    public UnityEvent onFailure;

    // Called when the command starts
    public virtual void StartCommand(IBattleActor user, IBattleActor target) { }

    // Called every frame during the command
    public virtual void UpdateCommand() { }

    // Called when the command completes
    public virtual void EndCommand() { }
}
