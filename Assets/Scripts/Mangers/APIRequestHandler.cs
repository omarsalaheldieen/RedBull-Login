using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class LoginManager
{
    private const string baseUrl = "https://staging.esportssummit-me.com/api/";

    public class LoginData
    {
        public string email;
        public string password;
    }

    public class ScoreData
    {
        public int score;
    }

    public static void MakeHttpRequest(string endpoint, string method, string jsonData, string token, Action<string, float> onSuccess, Action<string> onFailure)
    {
        CoroutineRunner.Instance.StartCoroutine(SendRequestCoroutine(endpoint, method, jsonData, token, onSuccess, onFailure));
    }

    private static IEnumerator SendRequestCoroutine(string endpoint, string method, string jsonData, string token, Action<string, float> onSuccess, Action<string> onFailure)
    {
        DateTime startTime = DateTime.UtcNow;

        using (UnityWebRequest request = new UnityWebRequest(baseUrl + endpoint, method))
        {
            if (method != UnityWebRequest.kHttpVerbGET && jsonData != null)
            {
                byte[] jsonToSend = System.Text.Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
            }

            yield return request.SendWebRequest();

            DateTime endTime = DateTime.UtcNow;
            float responseTime = (float)(endTime - startTime).TotalMilliseconds;

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
                onFailure?.Invoke(request.error);
            }
            else
            {
                onSuccess?.Invoke(request.downloadHandler.text, responseTime);
            }
        }
    }


    public static void Login(string email, string password, Action<string, string, int, int, float, bool> onSuccess, Action<string> onFailure, bool isAdmin = false)
    {
        LoginData loginData = new LoginData
        {
            email = email,
            password = password
        };

        string jsonData = JsonConvert.SerializeObject(loginData);

        MakeHttpRequest("auth/login", "POST", jsonData, null,
            (response, responseTime) =>
            {
                JObject loginResponse = JObject.Parse(response);
                string username = loginResponse["data"]?["user"]?["username"]?.ToString();
                string accessToken = loginResponse["data"]?["accessToken"]?.ToString();
                int uuid = loginResponse["data"]?["user"]?["uuid"]?.ToObject<int>() ?? 0;
                int id = loginResponse["data"]?["user"]?["id"]?.ToObject<int>() ?? 0;

                if (username != null && accessToken != null && uuid != 0)
                {
                   /* Debug.Log("Username: " + username);
                    Debug.Log("UUID: " + uuid);
                    Debug.Log("Response Time: " + responseTime + "ms");*/
                    onSuccess?.Invoke(username, accessToken, uuid, id, responseTime, isAdmin);
                }
                else
                {
/*                    Debug.LogError("Login failed: Invalid response structure");
*/                    onFailure?.Invoke("Invalid response structure");
                }
            },
            (error) =>
            {
                Debug.LogError("Login failed: " + error);
                onFailure?.Invoke(error);
            });
    }

    public static void UpdateScore(int score, int userId, Action<string, float> onSuccess, Action<string> onFailure)
    {
        ScoreData scoreData = new ScoreData
        {
            score = score
        };

        string jsonData = JsonConvert.SerializeObject(scoreData);

        if (string.IsNullOrEmpty(UserManager.AdminAccessToken))
        {
            onFailure?.Invoke("Admin not logged in");
            return;
        }
        
        MakeHttpRequest($"score/trivia/{userId}/game/3", "POST", jsonData, UserManager.AdminAccessToken,
            (response, responseTime) =>
            {
/*                Debug.Log("Score update successful");
                Debug.Log("Response Time: " + responseTime + "ms");*/
                onSuccess?.Invoke(response, responseTime);
            },
            (error) =>
            {
/*                Debug.LogError("Score update failed: " + error);
*/                onFailure?.Invoke(error);
            });
    }

    public static void GetScore(int userId, Action<int, string,float> onSuccess, Action<string> onFailure)
    {
        MakeHttpRequest($"profile/{userId}", "GET", null, UserManager.AccessToken,
            (response, responseTime) =>
            {
                try
                {
                    JObject profileResponse = JObject.Parse(response);
                    var scores = profileResponse["data"]?["scores"];
                    string rank = (string)profileResponse["data"]?["rank"];
/*                    Debug.Log("rank"  + rank);
*/                    if (scores != null)
                    {
                        int totalScore = scores.Sum(score => score["score"]?.ToObject<int>() ?? 0);
                        
                        UserManager.SetUserScore(totalScore , rank);
                        onSuccess?.Invoke(totalScore,rank , responseTime);
                    }
                    else
                    {
                        onFailure?.Invoke("Scores not found in response");
                    }
                }
                catch (Exception ex)
                {
                    onFailure?.Invoke("Failed to parse response: " + ex.Message);
                }
            },
            (error) =>
            {
                onFailure?.Invoke(error);
            });
    }

}

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("CoroutineRunner");
                _instance = obj.AddComponent<CoroutineRunner>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
}
