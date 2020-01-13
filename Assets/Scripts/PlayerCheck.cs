using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    [SerializeField]
    private PushableBox pushableBox;
    [SerializeField]
    private Rigidbody2D boxRB;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        pushableBox.checkPush = true;
        boxRB.bodyType = RigidbodyType2D.Static;
        print(boxRB.bodyType);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        pushableBox.checkPush = false;
        boxRB.bodyType = RigidbodyType2D.Dynamic;
        //DisableJoint();
    }
}
