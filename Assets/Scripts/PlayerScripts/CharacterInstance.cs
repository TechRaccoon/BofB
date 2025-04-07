using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInstance
{
    public string characterName;

    public int level;

    public int currentHP;
    public int maxHP;

    public int currentValor;
    public int MaxValor;

    public int attack;
    public int defense;

    public int experience;

    public CharacterInstance(CharacterTemplate template)
    {
        characterName = template.characterName;
        level = template.level;
        maxHP = template.baseHP;
        currentHP = maxHP;
        MaxValor = template.baseValor;
        currentValor = MaxValor;
        attack = template.baseAttack;
        defense = template.baseDefense;
        experience = 0;
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
        maxHP += 1;  
        currentHP = maxHP;
    }

    private int GetXPForNextLevel()
    {
        return level * 10; // Just a placeholder formula
    }
}

