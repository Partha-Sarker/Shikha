using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogObstacle : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private bool initialRight = true;
    [SerializeField]
    private GameObject dog;
    [SerializeField]
    private LightSwitch lightSwitch;
    
    private GameObject spawnedDog;
    private NPCMovement dogMovement;
    private bool dogSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "dog")
        {
            dogMovement.Flip();
        }
        if (dogSpawned)
            return;
        if (collision.tag != "Player")
            return;
        spawnedDog = Instantiate(dog, spawnPosition.position, Quaternion.identity);
        dogMovement = spawnedDog.GetComponent<NPCMovement>();
        if (!initialRight)
            dogMovement.Flip();
        dogMovement.Walk();
        dogSpawned = true;
    }
}
