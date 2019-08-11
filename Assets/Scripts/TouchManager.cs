using System;
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
    [SerializeField]
    private int actionTouchIndex = -1;
    [SerializeField]
    private float minSwipeDistance = 50;
    [SerializeField]
    private float maxSwipeTime = 1f;
    private float swipeStartTime;
    private Vector2 swipeStartPos;
    private PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        deviceWidth = Screen.width;
        deviceHeight = Screen.height;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        touchCount = Input.touchCount;

        for(int i = 0; i<touchCount; i++)
        {
            Touch touch = Input.touches[i];

            ConfigureMovementTouch(touch, i);
            
            ConfigureActionTouch(touch, i);

        }

        if(touchCount == 0)
        {
            movementTouchIndex = actionTouchIndex = -1;
        }
    }


    private void ConfigureMovementTouch(Touch touch, int i)
    {
        if (movementTouchIndex == -1 && touch.phase == TouchPhase.Began && touch.position.x < deviceWidth / 2)
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


        else if (i == movementTouchIndex)
        {
            float xDistance = touch.position.x - movTouchStartPos.x;

            input = Mathf.Clamp((xDistance / deviceWidth) * minMoveAmount, -1, 1);
            if (input > 0 && input < minInput)
                input = 0;
            else if (input < 0 && input > -minInput)
                input = 0;
        }
    }

    private void ConfigureActionTouch(Touch touch, int i)
    {
        if (actionTouchIndex == -1 && touch.phase == TouchPhase.Began && touch.position.x > deviceWidth / 2)
        {
            actionTouchIndex = i;
            swipeStartPos = touch.position;
            swipeStartTime = Time.time;
        }
        else if (i == actionTouchIndex && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            actionTouchIndex = -1;
            float timeDifference = Time.time - swipeStartTime;
            Vector2 endPos = touch.position;
            float distance = Vector2.Distance(swipeStartPos, endPos);
            print("delta time = " + timeDifference + " | distance = " + distance);
            if (timeDifference > maxSwipeTime || distance < minSwipeDistance)
                return;
            CheckSwipe(swipeStartPos, endPos);
        }

    }

    private void CheckSwipe(Vector2 swipeStartPos, Vector2 endPos)
    {
        Vector2 diff = endPos - swipeStartPos;
        float angle = Vector2.Angle(Vector2.right, diff);
        if(diff.y > 0 && angle > 45 && angle < 135)
        {
            playerMovement.JumpCheck();
        }
    }
}
