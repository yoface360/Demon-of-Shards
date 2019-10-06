using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    GameObject player;
    GameObject camera;
    Transform respawn;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        SceneManager.sceneLoaded += OnSceneLoaded;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnSceneLoaded(Scene sc, LoadSceneMode md)
    {
        if (sc.name != "Main Menu Scene")
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                camera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
            player.transform.position = respawn.transform.position;
            player.transform.rotation = respawn.transform.rotation;
            player.SendMessageUpwards("ChangeRespawnPoint", respawn.gameObject);
            //camera.GetComponent<CamController>().UpdateOffset();

        }
    }
    

}
