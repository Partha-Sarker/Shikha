using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int velocity = 5;
    [SerializeField]
    private bool facingRight = true;
    [SerializeField]
    private bool testMode = false;
    [SerializeField]
    private SceneLoader sceneLoader;
    public GameObject dog;
    public bool canCheck = true;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canCheck)
            return;
        if(collision.tag == "Player")
        {
            sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex, "You've been bitten");
        }
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
