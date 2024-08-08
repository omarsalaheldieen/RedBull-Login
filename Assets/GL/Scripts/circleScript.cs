using UnityEngine;

public class circleScript : MonoBehaviour
{
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    public GameObject circle; 
    public Rigidbody2D body;
    private Vector3 direction;
    public logicScript LogicScript;
    public bool birdIsAlive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        /*        circle.transform.Rotate(new Vector3(0, 0, 90)*Time.deltaTime);
        */

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) && birdIsAlive)
        {
            direction = Vector3.up * strength;
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            Debug.Log("Destroyed");
            birdIsAlive = false;

        }
        /*        birdIsAlive = false;
        */
    }

}
