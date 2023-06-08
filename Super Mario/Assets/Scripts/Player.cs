using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSprite smallRenderer;
    public PlayerSprite bigRenderer;

    private Death death;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead1 => death.enabled;

    private void Awake()
    {
        death = GetComponent<Death>();
    }

    public void Hit() //if mario gets hit by goomba 
    {
        if(big){
            Shrink();
        }else{
            Dead();
        }

    }

    private void Shrink()
    {}

    private void Dead()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        death.enabled = true; //if death is enabled then reset the level

        GameManager.Instance.ResetLevel(3f);
    }
}
