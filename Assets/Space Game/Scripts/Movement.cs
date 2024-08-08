using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject player;
    public float maxSpeed = 5f; // Maximum speed the player can reach
    public float acceleration = 0.1f; // How quickly the player accelerates
    public float deceleration = 0.1f; // How quickly the player decelerates
    private float currentSpeedX = 0f; // Current speed in the X direction
    private float currentSpeedY = 0f; // Current speed in the Y direction
    public bool isWalkingLeftTop = false;
    public bool isWalkingRightTop = false;
    public bool isWalkingleftBottom = false;
    public bool isWalkingRightBottom = false;

    public GameObject fireTopLeft;
    public GameObject fireTopRight;
    public GameObject fireBottomLeft;
    public GameObject fireBottomRight;

    void Start()
    {
        fireTopLeft.SetActive(false);
        fireTopRight.SetActive(false);
        fireBottomLeft.SetActive(false);
        fireBottomRight.SetActive(false);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        // top
        if (isWalkingLeftTop && isWalkingRightTop)
        {
            direction += Vector3.up;
        }

        // Right
        if (isWalkingRightBottom && isWalkingRightTop)
        {
            direction += Vector3.right;
        }

        // Bottom
        if (isWalkingRightBottom && isWalkingleftBottom)
        {
            direction += Vector3.down;
        }

        // Left
        if (isWalkingLeftTop && isWalkingleftBottom)
        {
            direction += Vector3.left;
        }

        // Right Top
        if (isWalkingRightTop)
        {
            direction += new Vector3(-1, -1, 0).normalized;
            fireTopRight.SetActive(true);
        }

        // Left Top
        if (isWalkingLeftTop)
        {
            direction += new Vector3(1, -1, 0).normalized;
            fireTopLeft.SetActive(true);
        }

        // Left bottom
        if (isWalkingleftBottom)
        {
            direction += new Vector3(1, 1, 0).normalized;
            fireBottomLeft.SetActive(true);
        }

        // Right bottom
        if (isWalkingRightBottom)
        {
            direction += new Vector3(-1, 1, 0).normalized;
            fireBottomRight.SetActive(true);
        }

        // Adjust the current speed based on the direction and acceleration/deceleration
        if (direction != Vector3.zero)
        {
            currentSpeedX = Mathf.MoveTowards(currentSpeedX, maxSpeed * direction.x, acceleration * Time.deltaTime);
            currentSpeedY = Mathf.MoveTowards(currentSpeedY, maxSpeed * direction.y, acceleration * Time.deltaTime);
        }
        else
        {
            fireTopLeft.SetActive(false);
            fireTopRight.SetActive(false);
            fireBottomLeft.SetActive(false);
            fireBottomRight.SetActive(false);
            currentSpeedX = Mathf.MoveTowards(currentSpeedX, 0, deceleration * Time.deltaTime);
            currentSpeedY = Mathf.MoveTowards(currentSpeedY, 0, deceleration * Time.deltaTime);
        }

        // Apply the movement
        player.transform.position += new Vector3(currentSpeedX, currentSpeedY, 0) * Time.deltaTime;
    }

    public void isTriggerLeftTop(bool canMoveLeft)
    {
        isWalkingLeftTop = canMoveLeft;
    }

    public void isTriggerRightTop(bool canMoveLeft)
    {
        isWalkingRightTop = canMoveLeft;
    }

    public void isTriggerLeftBottom(bool canMoveLeft)
    {
        isWalkingleftBottom = canMoveLeft;
    }

    public void isTriggerRightBottom(bool canMoveLeft)
    {
        isWalkingRightBottom = canMoveLeft;
    }
}
