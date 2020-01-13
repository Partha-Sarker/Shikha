using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool touchInput = false;
    private Rigidbody2D rb;
    private Animator animator;
    public float speed = 7f;
    private Vector2 moveSpeed = Vector2.zero;
    [HideInInspector]
    public float moveInput = 0f;
    public float flipDelay = .2f;
    private bool facingRight = true;
    public uint airControl = 20;
    public float jumpForce = 5f;
    [HideInInspector]
    public GameObject box;
    public bool isGrounded = true;
    public bool isPushing = false;
    public bool isCrouching = false;
    public LayerMask whatIsGround;
    private TouchManager touchManager;
    public Vector2 colliderSize;
    public bool canPush = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchManager = FindObjectOfType<TouchManager>();
    }

    void Update()
    {
        if(touchInput)
            moveInput = touchManager.input;
        else
            moveInput = Input.GetAxis("Horizontal");

        if (isPushing && !facingRight)
            animator.SetFloat("HSpeed", -moveInput);
        else
            animator.SetFloat("HSpeed", moveInput);

        if (moveInput == 0 || !isGrounded)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EnableBoxJoint();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            DisableBoxJoint();
        }

        if (Input.GetButtonDown("Jump"))
        {
            DisableBoxJoint();
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C))
            Crouch();

        if (Input.GetKeyUp(KeyCode.C))
            StandUp();
    }
    
    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        isGrounded = Physics2D.OverlapBox(position, colliderSize, 0, whatIsGround);

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            DisableBoxJoint();
            StandUp();
            animator.SetBool("isJumping", true);
        }

        if (isCrouching)
        {
            rb.velocity = new Vector2(moveInput * speed * .5f, velocity.y);
        }
        else if (isGrounded)
        {
            rb.velocity = new Vector2(moveInput * speed, velocity.y);
        }
        else if ((velocity.x < (speed/1.5) && moveInput > 0) || (velocity.x > -(speed / 1.5) && moveInput < 0))
        {
            Vector2 force = new Vector2(moveInput * airControl, 0);
            rb.AddForce(force);
        }

        if (!facingRight && rb.velocity.x > 0 && !isPushing)
        {
            if (flipDelay > 0)
                StartCoroutine("Flip");
            else
                FlipNow();
        }
        else if (facingRight && rb.velocity.x < 0 && !isPushing)
        {
            if (flipDelay > 0)
                StartCoroutine("Flip");
            else
                FlipNow();
        }
    }

    public void EnableBoxJoint()
    {
        if (!canPush)
            return;
        if (box != null)
        {
            DistanceJoint2D boxJoint = box.GetComponent<DistanceJoint2D>();
            boxJoint.enabled = true;
            Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
            boxRb.bodyType = RigidbodyType2D.Dynamic;
            //boxRb.gravityScale = 2;
        }
        isPushing = true;
        animator.SetBool("isPushing", true);
    }

    public void DisableBoxJoint()
    {
        if (!isPushing)
            return;
        if (box != null)
        {
            DistanceJoint2D boxJoint = box.GetComponent<DistanceJoint2D>();
            boxJoint.enabled = false;
            Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
            boxRb.bodyType = RigidbodyType2D.Static;
            //boxRb.gravityScale = 20;
        }
        isPushing = false;
        animator.SetBool("isPushing", false);
    }

    public void Crouch()
    {
        if (!isGrounded || isPushing)
            return;
        animator.SetBool("isCrouching", true);
        isCrouching = true;
    }

    public void StandUp()
    {
        if (!isCrouching)
            return;
        animator.SetBool("isCrouching", false);
        isCrouching = false;
    }

    public void Jump()
    {
        if (isGrounded && !isPushing && !isCrouching)
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
