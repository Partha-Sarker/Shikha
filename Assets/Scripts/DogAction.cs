using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAction : MonoBehaviour, IExecutableObject
{
    [SerializeField]
    private Rigidbody2D dogRB;
    [SerializeField]
    private Collider2D dogCol, detectCol;
    [SerializeField]
    private NPCMovement dogMovement;
    [SerializeField]
    private string requiredItem = "Stone";

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "light")
        {
            dogMovement.Stop();
            dogRB.bodyType = RigidbodyType2D.Kinematic;
            dogCol.enabled = false;
            dogMovement.canCheck = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "light")
        {
            dogCol.enabled = true;
            dogMovement.canCheck = true;
            dogMovement.Walk();
            dogRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public bool Execute(string itemName)
    {
        if(itemName == requiredItem)
        {
            dogMovement.Stop();
            dogRB.bodyType = RigidbodyType2D.Kinematic;
            dogCol.enabled = false;
            return true;
        }
        return false;
    }
}
