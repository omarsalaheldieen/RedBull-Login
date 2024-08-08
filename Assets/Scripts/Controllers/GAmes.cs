using UnityEngine;
using UnityEngine.SceneManagement;

public class GAmes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openCandy()
    {
/*        SceneManager.LoadScene(SceneData.candyCrush);
*/    }
    public void openBrid()
    {
        SceneManager.LoadScene(SceneData.brid);
    }
}
