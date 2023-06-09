using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private  Transform player; //reference to game object position, rotation
    public float height = 6.5f;
    public float undergroundHeight = -9.5f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate() //this will run after update and fixedupdate functiona are compiled
    {
        Vector3 cameraPosition = transform.position; //for current camera position
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); //for moving in only right direction
        transform.position = cameraPosition; //to update camera position
    }

    public void SetUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}
