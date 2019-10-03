using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int curBuildInd, bossBuildInd, firstLevelBuildInd;
    int titleBuildInd = 0;
    List<int> possibleLevels = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        curBuildInd = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(curBuildInd);
        bossBuildInd = curBuildInd + 1;
        firstLevelBuildInd = titleBuildInd + 2;
        Debug.Log(firstLevelBuildInd + " " + titleBuildInd + " " + curBuildInd);
        DetermineLevels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            ChangeLevel();
        }
    }

    int DetermineNextLevel()
    {
        int sceneToChoose = firstLevelBuildInd;
        int rand = Random.Range(0, 101);
        Debug.Log(rand);
        for (int i = firstLevelBuildInd; i <= possibleLevels.Count; i--)
        {
            if (rand < (100 / possibleLevels.Count * i))
            {
                sceneToChoose = i;
                break;
            }
        }
        return sceneToChoose;
    }
    void DetermineLevels()
    {
        int j = firstLevelBuildInd;
       for (int i = j; i < curBuildInd; i++)
        {
            possibleLevels.Add(i);
            Debug.Log("Build Index: " + i + " Scene Name: " + SceneManager.GetSceneByBuildIndex(i).name);
        }
        Debug.Log(j);
    }
    void ChangeLevel()
    {
        int levelToChange = DetermineNextLevel();
        Debug.Log(levelToChange);
        SceneManager.LoadScene(levelToChange);
    }
}
