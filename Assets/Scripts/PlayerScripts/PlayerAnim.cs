using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] Animator anim;

    public string[] staticDirections = { "Static_N", "Static_NW", "Static_W", "Static_SW", "Static_S", "Static_SE", "Static_E", "Static_NE" };
    public string[] walkDirections = { "Walk_N", "Walk_NW", "Walk_W", "Walk_SW", "Walk_S", "Walk_SE", "Walk_E", "Walk_NE" };
    public string[] attackDirection = { "Attack_NW", "Attack_NW", "Attack_SW", "Attack_SW", "Attack_SE", "Attack_SE", "Attack_NE", "Attack_NE" };
    public string[] damageDirection = { "DAMAGE_NW", "DAMAGE_NW", "Damage_SW", "Damage_SW", "DAMAGE_SE", "DAMAGE_SE", "DAMAGE_NE", "DAMAGE_NE" };
    public string[] deadDirections = { "DEAD_NW", "DEAD_NW", "DEAD_SW", "DEAD_SW", "DEAD_SW", "DEAD_SW", "DEAD_NW", "DEAD_NW" };
    int lastDirection;

    public void SetDirection(Vector2 newDirection, bool isMoving)
    {
        string[] directionArray = null;

        if (isMoving)
        {
            directionArray = walkDirections;
            lastDirection = DirectionToIndex(newDirection);
        }
        else {
            directionArray = staticDirections;
        }
        
        //play the selected anim based on state
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
