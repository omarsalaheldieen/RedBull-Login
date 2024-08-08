using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
public class FrameRateManager : MonoBehaviour
{
    [Header("Frame Settings")]
    int MaxRate = 9999;
    public float TargetFrameRate = 60.0f;
    float currentFrameTime;
    public TMP_Text frameRateText;

    void Update()
    {
        float currentFPS = 1.0f / Time.deltaTime;
        frameRateText.text = $"FPS: {currentFPS:F2}"; // Display FPS with two decimal places
    }

    void Awake()
    {
        QualitySettings.vSyncCount = 0; // Disable vSync
        Application.targetFrameRate = MaxRate;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
    }
   
    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / TargetFrameRate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));
            while (t < currentFrameTime)
                t = Time.realtimeSinceStartup;
        }
    }
}