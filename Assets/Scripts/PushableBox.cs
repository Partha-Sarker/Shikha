using UnityEngine;

public class PushableBox : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private DistanceJoint2D joint;
    private Rigidbody2D rb;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        joint = GetComponent<DistanceJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        playerMovement.canPush = true;
        if(playerMovement.box == null) playerMovement.box = this.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        playerMovement.canPush = false;
        joint.enabled = false;
        playerMovement.box = null;
        rb.gravityScale = 20;
    }

}
