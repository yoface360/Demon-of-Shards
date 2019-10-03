using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Transform respawn;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnSceneLoaded(Scene sc, LoadSceneMode md)
    {
       respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        player.transform.position = respawn.transform.position;
        player.transform.rotation = respawn.transform.rotation;
        player.SendMessageUpwards("ChangeRespawnPoint", respawn.gameObject);
    }
    

}
