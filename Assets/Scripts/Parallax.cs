using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private GameObject cam;
    public float parallexEffect;
    private float startPos;
    private float cameraPos;
    private float dist;
    private float newXPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        startPos = transform.position.x;
        cameraPos = cam.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        dist = (cam.transform.position.x - cameraPos) * parallexEffect;
        newXPosition = dist + startPos;
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }
}
