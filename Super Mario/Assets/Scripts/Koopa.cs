using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>(); 

           if(collision.transform.DotTest(transform, Vector2.down)) //to check whether mario lands on goomba head or not
           {
            EnterShell();
           }else{
            player.Hit();
           }
        }
    }

    private void OnTriggeredEnter2D(Collider2D other)
    {
        if(shelled && other.CompareTag("Player"))
        {
            if(!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();
                player.Hit();
            }
        }
        else if(!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
           Hit();
        }
    }

    private void EnterShell()
    {
        shelled = true;

        GetComponent<Entity>().enabled = false;
        GetComponent<Animations>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic= false;

        Entity movement = GetComponent<Entity>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell"); //for collision with goomba
    }

     private void Hit()
    {
        GetComponent<Animations>().enabled = false;
        GetComponent<Death>().enabled= true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if(pushed){
            Destroy(gameObject);
        }
    }

}
