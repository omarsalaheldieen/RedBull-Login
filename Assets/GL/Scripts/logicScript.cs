using TMPro;
using UnityEngine;

public class logicScript : MonoBehaviour
{
    public int playerScore;
    public TMP_Text scoreText; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [ContextMenu("Increas Score")]
    public void addScore()
    {
        playerScore = playerScore + 1;
        scoreText.text = playerScore.ToString();
    }
}
