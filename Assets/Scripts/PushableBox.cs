using UnityEngine;

public class PushableBox : MonoBehaviour
{
    private GameObject Player;
    private PlayerMovement playerMovement;
    private DistanceJoint2D joint;
    private Rigidbody2D rb;
    private bool checkPush = false;
    private bool facingPlayer = false;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        Player = playerMovement.gameObject;
        joint = GetComponent<DistanceJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        checkPush = true;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        checkPush = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        DisableJoint();
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

}
