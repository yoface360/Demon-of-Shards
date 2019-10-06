using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Devdog.General;

public class CountdownScript : MonoBehaviour
{
    [SerializeField]
    private Text displayText;
    private float curTime;
    float maxTime = 300f;
    bool inBossRoom = false;
    bool timerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        curTime = maxTime;
        StartTimer();
    }

    void StartTimer()
    {
        if(displayText != null)
        {
            displayText.text = "Time Left: 5:00";
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    void UpdateTimer()
    {
        if (timerRunning)
        {
            if (displayText != null)
            {
                curTime -= Time.deltaTime;
                string minutes = Mathf.Floor(curTime / 60).ToString("00");
                string seconds = Mathf.Floor(curTime % 60).ToString("00");
                displayText.text = "Time Left: " + minutes + ":" + seconds;
                if (curTime <= 0.0f)
                {
                    if (!inBossRoom)
                    {
                        SceneManager.LoadScene("Test Final Level");
                        curTime = maxTime;
                        inBossRoom = true;
                    }
                    else
                    {
                        ReturnToMainMenu();
                    }
                }
            }
        }
    }
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
        DontDestroyOnLoadManager.DestroyAllDoNotDestroyItems();
    }
    public void StopTimer()
    {
        timerRunning = false;
    }
    public void ResumeTimer()
    {
        timerRunning = true;
    }
    public void AddSeconds(float amount)
    {
        curTime += amount;
    }
}
