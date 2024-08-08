using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    [Header("Inputs")]
    public TMP_InputField emailTextbox;
    public TMP_InputField passTextbox;
    public TMP_Text emailText;
    public TMP_Text passText;
    public Button Button;
    public GameObject loading;
    public TMP_Text textError;

    void Start()
    {
        loading.SetActive(false);
    }

    void Update()
    {
    }

    public void SignIn()
    {
        if (string.IsNullOrEmpty(emailTextbox.text) && string.IsNullOrEmpty(passTextbox.text))
        {

        }
        else
        {
            Button.interactable = false;
            loading.SetActive(true);
            LoginManager.Login(emailTextbox.text, passTextbox.text, OnLoginSuccess, OnLoginFailure);
        }
    }

    private void OnLoginSuccess(string username, string accessToken, int uuid, int id ,  float responseTime , bool isAdmin)
    {
        
        UserManager.SetUserDetails(username, accessToken, uuid , id);
        SceneManager.LoadScene(SceneData.Home);
    }

    private void OnLoginFailure(string error)
    {
        Button.interactable = true;
        loading.SetActive(false);
/*        Debug.LogError("Login failed: " + error);
*/        textError.text = "Invalid email address or password.";
    }

    public void ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            emailText.text = "Value is required";
        }
        else if (IsValidEmail(email))
        {
            emailText.text = "";
/*            Debug.Log("Valid email format");
*/        }
        else
        {
            emailText.text = "Invalid email format";
        }
    }

    public void validatePass(string pass)
    {
        if (string.IsNullOrEmpty(pass))
        {
            passText.text = "Value is required";
        }
        else
        {
            passText.text = "";
        }
    }

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}
