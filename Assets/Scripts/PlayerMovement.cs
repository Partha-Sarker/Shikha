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
    [HideInInspector]
    public GameObject box;
    public bool isGrounded = true;
    public bool isPushing = false;
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

        moveInput = Input.GetAxis("Horizontal");
        //moveInput = touchManager.input;

        animator.SetFloat("HSpeed", moveInput);

        if (moveInput == 0 || !isGrounded)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        if (Input.GetKeyDown(KeyCode.E) && isPushing)
        {
            DisableBoxJoint();
        }
        else if (Input.GetKeyDown(KeyCode.E) && canPush)
        {
            EnableBoxJoint();
        }

        

        if (Input.GetButtonDown("Jump"))
        {
            DisableBoxJoint();
            JumpCheck();
        }
            

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

        if (facingRight == false && rb.velocity.x > 0 && !isPushing)
        {
            if (flipDelay > 0)
                StartCoroutine("Flip");
            else
                FlipNow();
        }
        else if (facingRight == true && rb.velocity.x < 0 && !isPushing)
        {
            if (flipDelay > 0)
                StartCoroutine("Flip");
            else
                FlipNow();
        }
    }

    void EnableBoxJoint()
    {
        if (box != null)
        {
            DistanceJoint2D boxJoint = box.GetComponent<DistanceJoint2D>();
            boxJoint.enabled = true;
            Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
            boxRb.gravityScale = 2;
        }
        isPushing = true;
        animator.SetBool("isPushing", isPushing);
    }

    void DisableBoxJoint()
    {
        if (box != null)
        {
            DistanceJoint2D boxJoint = box.GetComponent<DistanceJoint2D>();
            boxJoint.enabled = false;
            Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
            boxRb.gravityScale = 20;
        }
        isPushing = false;
        animator.SetBool("isPushing", isPushing);
    }

    public void JumpCheck()
    {
        if (isGrounded && !isPushing)
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
