using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownScript : MonoBehaviour
{
    [SerializeField]
    private Text displayText;
    private float totalTime = 300;

    // Start is called before the first frame update
    void Start()
    {
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
        if (displayText != null)
        {
            totalTime -= Time.deltaTime;
            string minutes = Mathf.Floor(totalTime / 60).ToString("00");
            string seconds = Mathf.Floor(totalTime % 60).ToString("00");
            displayText.text = "Time Left: " + minutes + ":" + seconds;
            if (totalTime <= 0.0f)
            {
                SceneManager.LoadScene("Main Menu Scene");
            }
        }
    }
}
