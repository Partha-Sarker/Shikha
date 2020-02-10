using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour, IParallaxRepeatable
{
    public static Transform player;
    public static Transform virtualCam;
    public static float virtualCameraInitialPos;
    public float parallexEffect;
    private float startPos;
    private float dist;
    private float newXPosition;
    [SerializeField]
    private bool willRepeat = true;
    [SerializeField]
    private bool randomOffset = false;
    [SerializeField]
    private int randomRepeatOffset = 20;
    //[SerializeField]
    //private float temp;
    //[SerializeField]
    //private int minRepeatDistance;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        virtualCameraInitialPos = virtualCam.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //temp = (cam.transform.position.x - cameraPos) * (1 - parallexEffect);
        dist = (virtualCam.position.x - virtualCameraInitialPos) * parallexEffect;

        newXPosition = dist + startPos;
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

    }

    public void Repeat(int repeatingDistance)
    {
        if (!willRepeat)
            return;
        if(randomOffset)
            repeatingDistance += Random.Range(0, randomRepeatOffset);
        if (player.position.x - transform.position.x > 0)
            startPos += repeatingDistance;
        else
            startPos -= repeatingDistance;
    }


    //public void ChangePosition()
    //{
    //    //if (temp > startPos + minRepeatDistance)
    //    //{
    //    //    startPos += repeatingDistance;
    //    //    this.enabled = false;
    //    //}
    //    //else if (temp < startPos - minRepeatDistance)
    //    //{
    //    //    startPos -= repeatingDistance;
    //    //    this.enabled = false;
    //    //}
    //}
}
