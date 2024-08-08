using UnityEngine;

public class pipeSpawner : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate;
    private float timer;
    public int higestOffset ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer+Time.deltaTime;
        }
        else
        {
            spawn();
            timer = 0;
        }
    }
    public void spawn()
    {
        float lowestPoint = transform.position.y - higestOffset;
        float higestPoint = transform.position.y + higestOffset;
        Instantiate(pipe, new Vector3( transform.position.x , Random.Range(lowestPoint ,higestPoint)), transform.rotation);
    }
}
