using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInstance : IBattleActor
{
    public CharacterTemplate Template { get; private set; }
    public int level;
    public int currentHP;
    public int currentValor;
    public int experience;
    public Sprite portrait;

    // Private backing field for MaxHP/Valor (to allow modification)
    public int maxHP;
    public int maxValor;

    public CharacterInstance(CharacterTemplate template)
    {
        Template = template;
        currentHP = template.baseHP;
        currentValor = MaxValor;
        level = template.level;
        experience = 0;
        portrait = template.portrait;
    }

    // IBattleEntity implementation (reads base stats directly from Template)
    public string CharacterName => Template.characterName;
    public int MaxHP
    {
        get => maxHP;
        set => maxHP = value;
    }
    public int MaxValor
    {
        get => maxValor;
        set => maxValor = value;
    }
    public int Attack => Template.baseAttack;
    public int Defense => Template.baseDefense;
    public RuntimeAnimatorController Animator => Template.animatorController;

    public bool IsDefeated
    {
        get => currentHP <= 0;
        set => currentHP = value ? 0 : maxHP;

    }

    public void GainExperience(int amount)
    {
        experience += amount;
        if (experience >= GetXPForNextLevel())
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        experience = 0;
        MaxHP += 1;  
        currentHP = MaxHP;
    }

    private int GetXPForNextLevel()
    {
        return level * 10; // Just a placeholder formula
    }

    private void TakeDamage() { }

    private void Healed() { }
}

