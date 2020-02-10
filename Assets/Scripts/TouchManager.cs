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
    [Range(0f, 1f)]
    public float swipePercent = .2f;
    private float minSwipeAmount;
    [HideInInspector]
    public float input = 0;
    [SerializeField]
    private float minInput = .4f;
    [SerializeField]
    private int actionTouchIndex = -1;
    //[SerializeField]
    //private float minSwipeDistance = 50;
    [SerializeField]
    private float maxSwipeTime = .25f;
    private float touchDownTime;
    private Vector2 touchDownPos;
    private PlayerMovement playerMovement;
    public bool isHolding = false;
    public bool isCrouching = false;


    // Start is called before the first frame update
    void Start()
    {
        deviceWidth = Screen.width;
        deviceHeight = Screen.height;
        minSwipeAmount = deviceHeight * swipePercent;
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.touchInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        touchCount = Input.touchCount;

        for(int i = 0; i<touchCount; i++)
        {
            Touch touch = Input.touches[i];

            ConfigureMovementTouch(touch, touch.fingerId);
            
            ConfigureActionTouch(touch, touch.fingerId);
            
        }

        if (touchCount == 0)
        {
            movementTouchIndex = actionTouchIndex = -1;
            input = 0;
            if (!isHolding)
            {
                isHolding = false;
                isCrouching = false;
                playerMovement.DisableBoxJoint();
            }
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
            touchDownPos = touch.position;
            touchDownTime = Time.time;
        }
        else if (i == actionTouchIndex && (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && isHolding == false)
        {
            float timeDifference = Time.time - touchDownTime;
            if (timeDifference > maxSwipeTime)
            {
                Vector2 touchUpPos = touch.position;
                float distance = Vector2.Distance(touchDownPos, touchUpPos);
                if (CheckDownSwipe(touchDownPos, touchUpPos))
                {
                    isCrouching = true;
                    playerMovement.Crouch();
                }
                else
                {
                    isHolding = true;
                    playerMovement.EnableBoxJoint();
                }
            }
        }
        else if (i == actionTouchIndex && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            actionTouchIndex = -1;
            Vector2 touchUpPos = touch.position;
            float distance = Vector2.Distance(touchDownPos, touchUpPos);
            if (isHolding)
            {
                isHolding = false;
                playerMovement.DisableBoxJoint();
                return;
            }
            if (isCrouching)
            {
                isCrouching = false;
                playerMovement.StandUp();
                return;
            }
            CheckUpSwipe(touchDownPos, touchUpPos);
        }

    }

    private void CheckUpSwipe(Vector2 swipeStartPos, Vector2 touchUpPos)
    {
        Vector2 diff = touchUpPos - swipeStartPos;
        float angle = Vector2.Angle(Vector2.right, diff);
        if(diff.y > minSwipeAmount && angle > 60 && angle < 120)
        {
            playerMovement.Jump();
        }
    }

    private bool CheckDownSwipe(Vector2 swipeStartPos, Vector2 touchUpPos)
    {
        Vector2 diff = touchUpPos - swipeStartPos;
        float angle = Vector2.Angle(Vector2.right, diff);

        if (diff.y > 0 || -diff.y < minSwipeAmount)
            return false;
        
        if (angle > 60 && angle < 120)
            return true;

        return false;
    }
}
