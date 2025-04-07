using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class EnemyTemplate : ScriptableObject
{
    public string EnemyName;

    public int baseHP;
    public int baseValor;
    public int baseAttack;
    public int baseDefense;

    public RuntimeAnimatorController animatorController;

}
