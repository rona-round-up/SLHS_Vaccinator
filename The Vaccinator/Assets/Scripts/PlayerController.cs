using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    Rigidbody2D rb;
    float moveInput;

    bool grounded = false;
    public Transform feetPos;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    public float jumpTime;
    float jumpTimer;
    bool jumping;

    public float jumpErrorMargin;
    float jumpPressedTimer = 0;

    Animator anim;

    public AudioSource source;
    public AudioClip[] jumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();
        GetInput();
    }

    void FixedUpdate()
    {
        SetVelocity();
    }

    void GetInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        jumpPressedTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            jumpPressedTimer = jumpErrorMargin;
        }

        if (grounded && jumpPressedTimer > 0)
        {
            if (!jumping)
            {
                source.clip = jumps[Random.Range(0, jumps.Length)];
                source.Play();
            }
            jumping = true;
            jumpTimer = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && jumping)
        {
            if (jumpTimer > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;
        }

        if (moveInput == 0 && grounded)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }

        if (jumping || !grounded)
        {
            anim.SetBool("jump", true);
            anim.SetBool("run", false);
        }
        if (grounded)  
        {
            anim.SetBool("jump", false);
        }
    }

    void SetVelocity()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void CheckGround()
    {
        if (Physics2D.OverlapCircle(feetPos.position, groundCheckRadius, groundLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
