using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>(); 

           if(player.starpower)
           {
            Hit();
           }
           else if(collision.transform.DotTest(transform, Vector2.down)) //to check whether mario lands on goomba head or not
           {
            Flatten();
           }else{
            player.Hit();
           }
        }
    }

    private void OnTriggeredEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Entity>().enabled = false;
        GetComponent<Animations>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f); //destroy goomba if mario land on his head
    }

    private void Hit()
    {
        GetComponent<Animations>().enabled = false;
        GetComponent<Death>().enabled= true;
        Destroy(gameObject, 3f);
    }



}
