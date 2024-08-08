using UnityEngine;

public class pipeMiddle : MonoBehaviour
{
    public logicScript LogicScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            LogicScript.addScore();
        }
    }
}
