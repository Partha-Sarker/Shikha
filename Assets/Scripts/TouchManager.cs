using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private int movementTouchIndex = -1;
    private Vector2 movTouchStartPos;
    private int touchCount = 0;
    private int deviceHeight, deviceWidth;
    [SerializeField]
    private int minMoveAmount = 10;
    [HideInInspector]
    public float input = 0;
    [SerializeField]
    private float minInput = .4f;

    // Start is called before the first frame update
    void Start()
    {
        deviceWidth = Screen.width;
        deviceHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        touchCount = Input.touchCount;

        for(int i = 0; i<touchCount; i++)
        {
            Touch touch = Input.touches[i];

            if (movementTouchIndex == -1 && touch.phase == TouchPhase.Began)
            {
                movementTouchIndex = i;
                movTouchStartPos = touch.position;
                input = 0;
            }
                

            else if (i == movementTouchIndex && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                movementTouchIndex = -1;
                input = 0;
            }
                

            else if(i == movementTouchIndex)
            {
                float xDistance = touch.position.x - movTouchStartPos.x;

                input = Mathf.Clamp((xDistance / deviceWidth) * minMoveAmount, -1, 1);
                if (input > 0 && input < minInput)
                    input = 0;
                else if (input < 0 && input > -minInput)
                    input = 0;
            }
            else
            {
                input = 0;
            }
        }
    }
}
