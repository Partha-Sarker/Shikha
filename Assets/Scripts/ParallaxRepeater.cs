using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxRepeater : MonoBehaviour
{

    [SerializeField]
    private int repeatingDistance = 50;
    private IParallaxRepeatable repeater;


    private void OnTriggerExit2D(Collider2D collision)
    {
        repeater = collision.GetComponent<IParallaxRepeatable>();
        if (repeater != null)
            repeater.Repeat(repeatingDistance);
    }
}
