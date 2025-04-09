using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInstance : IBattleActor //enemy instanciation class ONLY for battle 
{
    public EnemyTemplate Template { get; private set; }
    public int currentHP;
    public int currentValor;
 
    public int maxHP;
    public int maxValor;

    public RuntimeAnimatorController animator;

    public EnemyInstance(EnemyTemplate template)
    {
        Template = template;
        currentHP = maxHP;
        currentValor = MaxValor;
    }

    // IBattleEntity implementation (reads base stats directly from Template)
    public string CharacterName => Template.EnemyName;
    public int MaxHP { get => maxHP; set => throw new System.NotImplementedException(); }
    public int MaxValor { get => maxValor; set => throw new System.NotImplementedException(); }
    public int Attack => Template.baseAttack;
    public int Defense => Template.baseDefense;
    public RuntimeAnimatorController Animator => Template.animatorController;
    public bool IsDefeated
    {
        get => currentHP <= 0;
        set => currentHP = value ? 0 : maxHP;

    }
}
