using UnityEngine;
using System.Collections;
using System;

public class PushableBox : MonoBehaviour, IExecutableObject
{
    private GameObject Player;
    private PlayerMovement playerMovement;
    private DistanceJoint2D joint;
    [HideInInspector]
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite brokenBox;

    private bool isBroken = false;
    public bool checkPush = false;
    private bool facingPlayer = false;
    private bool isGrounded;
    private bool wasGrounded;

    public double currentYVel;
    public double prevYVel = -1;

    [SerializeField]
    private int disappearDelay = 5;
    [SerializeField]
    private float breakingDelay = .5f;
    [SerializeField]
    private int breakingVelocity = -20;
    [SerializeField]
    private Vector2 colliderSize;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Collider2D[] cols;
    [SerializeField]

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        Player = playerMovement.gameObject;
        joint = GetComponent<DistanceJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isBroken)
            return;

        isGrounded = Physics2D.OverlapBox(transform.position, colliderSize, 0, whatIsGround);

        if (!isGrounded && checkPush)
        {
            playerMovement.DisableBoxJoint();
            playerMovement.canPush = false;
            checkPush = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (checkPush)
        {
            float distance = transform.position.x - Player.transform.position.x;
            float scaler = Player.transform.localScale.x;
            if((distance > 0 && scaler>0) || (distance <0 && scaler < 0))
            {
                EnableJoint();
            }
            else if(facingPlayer)
            {
                DisableJoint();
            }
        }
    }

    private void EnableJoint()
    {
        facingPlayer = true;
        playerMovement.canPush = true;
        if (playerMovement.box == null) playerMovement.box = this.gameObject;
    }

    private void DisableJoint()
    {
        facingPlayer = false;
        playerMovement.canPush = false;
        joint.enabled = false;
        playerMovement.box = null;
        //rb.gravityScale = 20;
    }

    public bool Execute(string itemName)
    {
        if(itemName == "Stone")
        {
            Break();
            return true;
        }
        return false;
    }

    private void Break()
    {
        StartCoroutine(Breaking());
    }

    IEnumerator Breaking()
    {
        isBroken = true;
        spriteRenderer.sprite = brokenBox;
        spriteRenderer.sortingOrder = 1;
        joint.enabled = false;
        yield return new WaitForSeconds(breakingDelay);
        rb.bodyType = RigidbodyType2D.Static;
        rb.velocity = Vector2.zero;
        foreach (Collider2D col in cols)
            col.enabled = false;
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}
