using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxGroundCheck : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField]
    private Rigidbody2D boxRB;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            playerMovement.DisableBoxJoint();
            //boxRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
