using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; //reference to sprite renderer
    private Movements movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Animations run;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //to refer to current sprite and movement script
        movement = GetComponentInParent<Movements>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate() //for different sprites of mario while movement
    {
        run.enabled = movement.running;

       if(movement.jumping){
        spriteRenderer.sprite = jump;
       }else if(movement.sliding){
        spriteRenderer.sprite = slide;
       }else if(!movement.running){
        spriteRenderer.sprite = idle;
       }
    }
}
