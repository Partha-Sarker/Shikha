using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    public float speed = 7f;
    private Vector2 moveSpeed = Vector2.zero;
    private float moveInput = 0f;
    public float flipDelay = .2f;
    private bool facingRight = true;
    public uint airControl = 20;
    public float jumpForce = 5f;
    public float jumpDelay = .1f;
    public bool isGrounded = true;
    public LayerMask whatIsGround;
    private TouchManager touchManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchManager = FindObjectOfType<TouchManager>();
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (isGrounded)
        {
            rb.velocity = new Vector2(moveInput * speed, velocity.y);
        }
        else if ((velocity.x < (speed/1.5) && moveInput > 0) || (velocity.x > -(speed / 1.5) && moveInput < 0))
        {
            Vector2 force = new Vector2(moveInput * airControl, 0);
            rb.AddForce(force);
        }

        if (facingRight == false && rb.velocity.x > 0)
        {
            if (flipDelay > 0)
                StartCoroutine("Flip");
            else
                FlipNow();
        }
        else if (facingRight == true && rb.velocity.x < 0)
        {
            if (flipDelay > 0)
                StartCoroutine("Flip");
            else
                FlipNow();
        }
    }

    void Update()
    {

        //moveInput = Input.GetAxis("Horizontal");
        moveInput = touchManager.input;
        animator.SetFloat("HSpeed", moveInput);

        if (moveInput == 0 || !isGrounded)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        if (Input.GetButtonDown("Jump"))
            JumpCheck();
            
    }

    public void JumpCheck()
    {
        if (isGrounded)
        {
            animator.SetTrigger("takeOff");
            StartCoroutine("Jump");
        }
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(jumpDelay);
        rb.velocity = Vector2.up * jumpForce;
    }

    IEnumerator Flip()
    {
        yield return new WaitForSeconds(flipDelay);
        if (facingRight == false && rb.velocity.x > 0)
        {
            FlipNow();
        }
        else if (facingRight == true && rb.velocity.x < 0)
        {
            FlipNow();
        }
    }

    private void FlipNow()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


}
