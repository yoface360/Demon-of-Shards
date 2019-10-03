using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempSceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            LoadBossLevel();
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            LoadInterimScene();
        }
    }

    void LoadBossLevel()
    {
        SceneManager.LoadScene("Test Final Level");
    }
    
    void LoadInterimScene()
    {
        SceneManager.LoadScene("Interim");
    }
}
