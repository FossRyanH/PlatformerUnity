using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator anim;
    BoxCollider2D collider;
    
    int jumpCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        if (collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpCount = 0;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    void OnJump(InputValue value)
    {
        if (value.isPressed && jumpCount < 1)
        {
            jumpCount++;
            rb.velocity += new Vector2(rb.velocity.x, jumpForce);
        }
    }
    
    void Run()
    {
        bool hasMovementSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        
        anim.SetBool("isRunning", hasMovementSpeed);
    }
    
    void FlipSprite()
    {
        bool hadHorizontalInput = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (hadHorizontalInput)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
