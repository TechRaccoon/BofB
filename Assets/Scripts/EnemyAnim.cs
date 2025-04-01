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

    public string[] idleDirection = { "IDLE_NW", "IDLE_SW", "IDLE_SE", "IDLE_NE" };
    public string[] runDirection = { "RUNNING_SE", "RUNNING_NE", "RUNNING_NW", "RUNNING_SW" };
    //public string[] attackDirection = { "ATTACK_NW", "ATTACK_NW", "ATTACK_SW", "ATTACK_SW", "ATTACK_SE", "ATTACK_SE", "ATTACK_NE", "ATTACK_NE" };
    //public string[] damageDirection = { "DAMAGE_NW", "DAMAGE_NW", "DAMAGE_SW", "DAMAGE_SW", "DAMAGE_SE", "DAMAGE_SE", "DAMAGE_NE", "DAMAGE_NE" };
    //public string[] deathDirection = { "DEATH_NW", "DEATH_NW", "DEATH_SW", "DEATH_SW", "DEATH_SE", "DEATH_SE", "DEATH_NE", "DEATH_NE" };
    //public string[] deathDirections = { "DEATH_NW", "DEATH_NW", "DEATH_SW", "DEATH_SW", "DEATH_SE", "DEATH_SE", "DEATH_NE", "DEATH_NE" };

    private int lastDirection;


    public void SetDirection(Vector2 newDirection)
    {
        string[] directionArray = null;

        switch (state.GetEnemyState())
        {
            //case EnemyState.ATTACK:
            //directionArray = attackDirection;
            //lastDirection = DirectionToIndex(newDirection);
            //break;
            //case EnemyState.DAMAGE:
            //if (hasDamageAnim)
            //{
            //directionArray = damageDirection;
            //lastDirection = DirectionToIndex(newDirection);
            //}
            //else { directionArray = idleDirection; }
            //break;
            //case EnemyState.DEAD:
            //directionArray = deathDirections;
            //lastDirection = DirectionToIndex(newDirection);
            //break;
            case EnemyState.CHASE:
                //Debug.Log(runDirection[0]);
                directionArray = runDirection;
                lastDirection = DirectionToIndex(newDirection);
                break;
            case EnemyState.IDLE:
                directionArray = idleDirection;
                lastDirection = DirectionToIndex(newDirection);
                break;
            default: break;
        }
        anim.Play(directionArray[lastDirection]);
        //anim.Play(runDirection[3]);

    }
    private int DirectionToIndex(Vector2 newDirection)
    {
        Vector2 noDir = newDirection.normalized;
        float angle = Mathf.Atan2(noDir.y, noDir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        // Map to 4 directions: SE (Right), NE (Up), NW (Left), SW (Down)
        if (angle >= 315f || angle < 45f) return 0;   // SE (Right)
        if (angle >= 45f && angle < 135f) return 1;   // NE (Up)
        if (angle >= 135f && angle < 225f) return 2;  // NW (Left)
        return 3;                                     // SW (Down)
    }


}