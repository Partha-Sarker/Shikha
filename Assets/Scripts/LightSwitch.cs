using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject light;
    private bool isLightOn = false;
    [SerializeField]
    private float switchOffDelay = 3f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLightOn)
            return;
        if (collision.tag == "Player")
        {
            light.SetActive(true);
            Invoke("TurnLightOff", switchOffDelay);
            isLightOn = true;
        }
    }

    private void TurnLightOff()
    {
        light.SetActive(false);
        isLightOn = false;
    }
}
