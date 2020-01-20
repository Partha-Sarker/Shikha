using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EndOfExploration : MonoBehaviour
{
    public CinemachineVirtualCamera cam;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //cam.LookAt = transform;
            cam.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //cam.LookAt = null;
            cam.enabled = true;
        }
    }
}
