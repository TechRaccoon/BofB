using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInstance //enemy instanciation class ONLY for battle 
{
    public string enemyName;

    public int currentHP;
    public int maxHP;

    public int currentValor;
    public int MaxValor;

    public int attack;
    public int defense;

    public EnemyInstance(EnemyTemplate template)
    {
        // Setting up the stats for battle 
        enemyName = template.EnemyName;
        maxHP = template.baseHP;
        currentHP = maxHP;
        MaxValor = template.baseValor;
        currentValor = MaxValor;
        attack = template.baseAttack;
        defense = template.baseDefense;
    }
}
