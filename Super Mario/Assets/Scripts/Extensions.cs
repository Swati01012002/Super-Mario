using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    private static LayerMask layerMask = LayerMask.GetMask("Default"); //it will ignore any objects that are not on the default layer ; Mario is set on player level to keep it ignored

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction) //returns true if a target is hit or game object collides
    {
        if(rigidbody.isKinematic){ //if the game object is stationary
            return false;
        }
        float radius = 0.25f; //circle cast
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask); //RaycastHit2D store info of what you hit
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection) //transform : mario ; other : blocks
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}

