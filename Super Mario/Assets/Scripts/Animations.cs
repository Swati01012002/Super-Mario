using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f; //6 frames a second

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate); //for repeatition of animation for x amount of time
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;
        if(frame >= sprites.Length){ //for run animation
            frame = 0;
        }

        if(frame >= 0 && frame <sprites.Length){
            spriteRenderer.sprite = sprites[frame];
        }
    }
}
