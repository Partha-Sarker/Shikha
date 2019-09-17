using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushBoxGroundCheck : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("mid air");
        if (collision.gameObject.tag == "ground")
        {
            playerMovement.DisableBoxJoint();
        }
    }
}
