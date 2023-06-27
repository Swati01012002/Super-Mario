using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private float inputAxis;
    private Vector2 velocity;

    public float moveSpeed = 0f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float jumpForce => (2f * maxJumpHeight)/(maxJumpTime / 2f); //=> this is a property with a get
    public float gravity => (-2f * maxJumpHeight)/Mathf.Pow((maxJumpTime / 2f), 2); //ms-2

    public bool grounded { get; private set; } //getter to read it from other scripts
    public bool jumping { get; private set; } //setting can be done only by present script
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    private void Awake()  //unity will call this function when script is first initialized
    {
        rigidbody = GetComponent<Rigidbody2D>(); //to search for component on which script is to be applied
        camera = Camera.main; //unity built in func to access camera
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void Update()
    {
        HorizontalMovement(); 

       grounded = rigidbody.Raycast(Vector2.down); //to check if we are grounded

       if(grounded){
        GroundedMovement();

       }
       ApplyGravity();

    }

    private void HorizontalMovement()
    {
       inputAxis = Input.GetAxis("Horizontal"); //for taking input
       velocity.x = Mathf.MoveTowards(velocity.x, inputAxis*moveSpeed, moveSpeed * Time.deltaTime); //for velocity of game object

       if(rigidbody.Raycast(Vector2.right * velocity.x)){
        velocity.x = 0f; //to change velocity to zero if object collides to the wall in x direction
       }

       if(velocity.x > 0f){ //for rotation of mario's face only in y axis
        transform.eulerAngles = Vector3.zero;
       }else if(velocity.x < 0f){
        transform.eulerAngles = new Vector3(0f, 180f, 0f);

       }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f); //velocity will be either positive while moving upwards or zero while grounding

        jumping = velocity.y > 0f;
        if(Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce; //for jumping
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f; //to fall faster a value is multiplied
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f); //terminal velocity to stop y velocity from reaching too large of a value
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position; //for changing position of object while moving
        position += velocity * Time.fixedDeltaTime;
        
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero); //to bound the camera from both edges 
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f); //set x axis between the both edges

        rigidbody.MovePosition(position); //unity function to change position
    }

    private void OnCollisionEnter2D(Collision2D collision) //unity built-in func to prevent mario movement when the block is above him
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f; //when mario hits goomba
                jumping = true;
            }
        }
        
        if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
           if(transform.DotTest(collision.transform, Vector2.up)){
            velocity.y = 0f;
           }
        }
    }


}
