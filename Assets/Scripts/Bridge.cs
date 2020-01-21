using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField]
    private float breakingTime = 1.5f;

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
        GetComponent<Collider2D>().enabled = false;
        gameObject.AddComponent<Rigidbody2D>();
        Destroy(this.gameObject, 4);
    }
}
