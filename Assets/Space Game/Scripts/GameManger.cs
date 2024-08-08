using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public Button start;
    public Button exit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        Debug.Log("Done");
        SceneManager.LoadScene(SceneData.Game);
    }

    public void end()
    {
        Debug.Log("exit");
    }
}
