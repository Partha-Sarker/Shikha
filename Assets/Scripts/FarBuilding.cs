using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarBuilding : MonoBehaviour
{
    public Transform cam;
    public Vector3 ini;

    // Start is called before the first frame update
    void Start()
    {
        ini = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ini.x = cam.position.x;
        transform.position = ini;
    }
}
