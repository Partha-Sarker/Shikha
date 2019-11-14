using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private GameObject cam;
    public float parallexEffect;
    private float startPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
