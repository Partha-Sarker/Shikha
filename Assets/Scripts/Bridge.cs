using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField]
    private float breakingTime = 1.5f;
    [SerializeField]
    private float gravityScale = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            print("Breaking");
            Invoke("Break", breakingTime);
        }
    }

    private void Break()
    {
        //GetComponent<Collider2D>().enabled = false;
        gameObject.AddComponent<Rigidbody2D>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        Destroy(this.gameObject, 4);
    }
}
