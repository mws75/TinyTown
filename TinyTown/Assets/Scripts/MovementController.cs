using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //properties
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    bool facingRight = true;

    Animator animator;

    string animationState = "AnimationState";
    Rigidbody2D rigidbody;
    SpriteRenderer renderer;

    enum CharStates
    {
        idlesouth = 0,
        idleNorth = 5,

        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4       
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();

        if(movement.x > 0 && !facingRight)
        {
            Flip();
        }
        if(movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rigidbody.velocity = movement * movementSpeed;
    }

    void Flip()
    {
        facingRight = !facingRight;
        renderer.flipX = !renderer.flipX;

        Debug.Log("Flip");
    }

    private void UpdateState()
    {
        if (movement.x > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkEast);
            Debug.Log("walk east");
        }
        else if (movement.x < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkWest);
            Debug.Log("walk west");
        }
        else if (movement.y > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkNorth);
            Debug.Log("walk north");
        }
        else if (movement.y < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.walkSouth);           
            Debug.Log("walk south");
        }
        else
        {
            animator.SetInteger(animationState, (int)CharStates.idlesouth);
            Debug.Log("Idle");
        }
    }
}
