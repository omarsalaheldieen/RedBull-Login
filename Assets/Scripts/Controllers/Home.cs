using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Uuid;
    public TMP_Text score;
    public TMP_Text Rank;
    public Button Button;
    public GameObject loading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Retrieve the username from UserManager and set it to the TMP_Text component
        Name.text = "Name: " + UserManager.Username;
        Uuid.text = "UUID: " + UserManager.Uuid;
        score.text = "Score: " + UserManager.Score;
        Rank.text = "Rank: " + UserManager.Rank;
        

        LoginManager.GetScore(UserManager.Id, OnGetScoreSuccess, OnGetScoreFailure);

        // Login as admin
        if (UserManager.AdminAccessToken == null)
        {
            LoginManager.Login("a@gamersloungeme.com", "GamersLounge", OnLoginSuccess, OnLoginFailure, true);

        }
        else
        {
            Button.interactable = true;
            loading.SetActive(false);
        }
    }
    private void OnGetScoreSuccess(int totalScore, string rank , float responseTime)
    {
        UserManager.SetUserScore(totalScore  , rank);
        score.text = "Score: " + UserManager.Score;
        Rank.text = "Rank: " + UserManager.Rank;

    }

    private void OnGetScoreFailure(string error)
    {

    }
    private void OnLoginSuccess(string username, string accessToken, int uuid, int id ,  float responseTime, bool isAdmin)
    {
        UserManager.SetAdminAccessToken(accessToken);
/*        Debug.Log(UserManager.AdminAccessToken);
*/        Button.interactable = true;
        loading.SetActive(false);
    }

    private void OnLoginFailure(string error)
    {

        Debug.LogError("Login failed: " + error);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startPlaying()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene(SceneData.flappyBird);
    }
}
