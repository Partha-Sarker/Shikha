using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]
    private int velocity = 5;
    [SerializeField]
    private bool facingRight = true;
    [SerializeField]
    private bool testMode = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //Update is called once per frame
    void Update()
    {
        if (!testMode)
            return;
        if (Input.GetKeyDown(KeyCode.J))
            Walk();
        if (Input.GetKeyDown(KeyCode.K))
            Stop();
        if (Input.GetKeyDown(KeyCode.F))
            Flip();
    }

    public void Walk()
    {
        animator.SetBool("Walking", true);
        if (facingRight)
            velocity = Mathf.Abs(velocity);
        else
            velocity = -Mathf.Abs(velocity);
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }

    public void Flip()
    {
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        Vector3 scaler = transform.localScale;

        if (facingRight)
            scaler.x = -1;
        else
            scaler.x = 1;

        facingRight = !facingRight;
        transform.localScale = scaler;
    }

    public void Stop()
    {
        animator.SetBool("Walking", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}
