using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnim : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] EnemyAI state;
    [SerializeField] bool hasDamageAnim = false;

    public string[] idleDirection = { "IDLE_NW", "IDLE_NW", "IDLE_SW", "IDLE_SW", "IDLE_SE", "IDLE_SE", "IDLE_NE", "IDLE_NE" };
    public string[] runDirection = { "RUNNING_NW", "RUNNING_NW", "RUNNING_SW", "RUNNING_SW", "RUNNING_SE", "RUNNING_SE", "RUNNING_NE", "RUNNING_NW" };
    public string[] attackDirection = { "ATTACK_NW", "ATTACK_NW", "ATTACK_SW", "ATTACK_SW", "ATTACK_SE", "ATTACK_SE", "ATTACK_NE", "ATTACK_NE" };
    public string[] damageDirection = { "DAMAGE_NW", "DAMAGE_NW", "DAMAGE_SW", "DAMAGE_SW", "DAMAGE_SE", "DAMAGE_SE", "DAMAGE_NE", "DAMAGE_NE" };
    public string[] deathDirection = { "DEATH_NW", "DEATH_NW", "DEATH_SW", "DEATH_SW", "DEATH_SE", "DEATH_SE", "DEATH_NE", "DEATH_NE" };
    public string[] deathDirections = { "DEATH_NW", "DEATH_NW", "DEATH_SW", "DEATH_SW", "DEATH_SE", "DEATH_SE", "DEATH_NE", "DEATH_NE" };

    private int lastDirection;


    public void SetDirection(Vector2 newDirection)
    {
        string[] directionArray = null;

        switch (state.GetEnemyState())
        {
            case EnemyState.ATTACK:
                directionArray = attackDirection;
                lastDirection = DirectionToIndex(newDirection);
                break;
            case EnemyState.CHASE:
                //Debug.Log(runDirection[0]);
                directionArray = runDirection;
                lastDirection = DirectionToIndex(newDirection);
                break;
            case EnemyState.IDLE:
                directionArray = idleDirection;
                //lastDirection = DirectionToIndex(newDirection);
                break;
            case EnemyState.DAMAGE:
                if (hasDamageAnim)
                {
                    directionArray = damageDirection;
                    lastDirection = DirectionToIndex(newDirection);
                }
                else { directionArray = idleDirection; }
                break;
            case EnemyState.DEAD:
                directionArray = deathDirections;
                lastDirection = DirectionToIndex(newDirection);
                break;
            default: break;
        }
        anim.Play(directionArray[lastDirection]);

    }
    private int DirectionToIndex(Vector2 newDirection)
    {
        Vector2 noDir = newDirection.normalized;

        //counter clockwise 8-direction movement
        float step = 360 / 8;
        float offSet = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, noDir);

        angle += offSet;
        if (angle < 0)
        {
            angle += 360;
        }
        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }


}