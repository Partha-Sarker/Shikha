using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{

    public Transform player;
    private Vector2 playerInitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerInitialPosition = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            player.position = playerInitialPosition;
    }
}
