using System;
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
    public bool enemy;

    // Private backing field for MaxHP/Valor (to allow modification)
    public int maxHP;
    public int maxValor;

    // Event for the HUD listener
    public event Action<int> OnHealthChanged;
    public event Action<int> OnValorChanged;
    public event Action OnCharacterDeath;

    // First Insnciation of the character (On creation)
    public CharacterInstance(CharacterTemplate template)
    {
        Template = template;
        currentHP = template.baseHP;
        currentValor = template.baseValor;
        level = template.level;
        experience = 0;
        portrait = template.portrait;
        enemy = false;
        maxHP = template.baseHP;
        maxValor = template.baseValor;
    }

    // IBattleEntity implementation (reads base stats directly from Template)
    public string CharacterName => Template.characterName;

    public int MaxHP
    {
        get => maxHP;
        set => maxHP = currentHP;
    }

    public int MaxValor
    {
        get => maxValor;
        set => maxValor = currentValor;
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

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        OnHealthChanged?.Invoke(currentHP);
    }

    public void SpendValor(int cost)
    {
        currentValor -= cost;
        OnValorChanged?.Invoke(currentValor);
    }

    public void Healed(int heal)
    {
        currentHP += heal;
        if (currentHP > maxHP) currentHP = maxHP;
        OnHealthChanged?.Invoke(currentHP);
    }

    public void RestoreValor(int restore)
    {
        currentValor += restore;
        if (currentValor > maxValor) currentValor = maxValor;
        OnValorChanged?.Invoke(currentValor);
    }
}

