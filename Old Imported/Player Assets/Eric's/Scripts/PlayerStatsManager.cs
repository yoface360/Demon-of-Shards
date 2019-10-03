using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.InventoryPro;
using Devdog.General;


public class PlayerStatsManager : MonoBehaviour
{
    //Prefab for player Respawns
    public GameObject prefabPlayer;
    //Point to respawn in case player dies
    GameObject respawnPoint;


    float deadTime = 1.3f;
    float respawnTime = 3.5f;
    GameObject player;


    InventoryPlayer curPlayer;

    // Start is called before the first frame update
    void Start()
    {
        curPlayer = PlayerManager.instance.currentPlayer.inventoryPlayer;
        GetComponentInChildren<Player>().inventoryPlayer.stats.Get("Level", "Health").OnValueChanged += CheckHealth;
        GetComponentInChildren<Player>().inventoryPlayer.stats.Get("Level", "Level").OnLevelChanged += LevelUp;
       // curPlayer.stats.Get("Level", "Level").SetLevel(0, true, true);
       // curPlayer.stats.Get("Level", "Level").SetExperience(0f, true);
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("XP: " + curPlayer.stats.Get("Level", "Level").currentExperience);
    }
    private void Update()
    {
       // Debug.Log("XP: " + curPlayer.stats.Get("Level", "Level").currentExperience);
      //  Debug.Log("Level: " + curPlayer.stats.Get("Level", "Level").currentValue);
      if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(-100f);
        }

    }
    // Update is called once per frame
    public void TakeDamage(float amount)
    {
        curPlayer.stats.Get("Level", "Health").ChangeCurrentValueRaw(-amount);
        Debug.Log(curPlayer.stats.Get("Level", "Health").currentValue);
        

    }
    void CheckHealth(IStat stat)
    {
        if (stat.currentValue <= 0)
        {
            SendMessage("IsPlayerAlive", false);
            StartCoroutine("PlayerDeath");
        }
    }
    public void GainExperience (float amt)
    {
        curPlayer.stats.Get("Level", "Level").ChangeExperience(amt);
    }

    void LevelUp(IStat stat)
    {
        curPlayer.stats.Get("Level", "Health").IncreaseLevel(true, true);
        curPlayer.stats.Get("Level", "Health").SetCurrentValueRaw(curPlayer.stats.Get("Level", "Health").currentMaxValue);
    }

    /// <summary>
    /// Code for basic player death/respawns
    /// </summary>
    IEnumerator PlayerDeath()
    {
        // We can either Destroy the player, or just set him as inactive and reset his transform, I reset his activity to make it easier for now
        BroadcastMessage("DeadAnim", true);
        yield return new WaitForSecondsRealtime(deadTime);
        
        player.SetActive(false);
        yield return new WaitForSecondsRealtime(respawnTime);
        transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        player.transform.SetPositionAndRotation(respawnPoint.transform.position, Quaternion.identity);
        player.SetActive(true);
        BroadcastMessage("ResetTrapCoolDown");

        // Put Camera Replacement Code here


        //The Destroying and then re-instantiating the player method
        /* Destroy(player);
        yield return new WaitForSecondsRealtime(3.5f);
        Instantiate(prefabPlayer, respawnPoint.transform.position, Quaternion.identity);
        */

        ResetHealth();
    }
    /// <summary>
    /// Resets Health after the player respawns
    /// </summary>
    void ResetHealth()
    {
        curPlayer.stats.Get("Level", "Health").SetCurrentValueRaw(curPlayer.stats.Get("Level", "Health").currentMaxValue);
        SendMessage("IsPlayerAlive", true);
    }
    void ChangeRespawnPoint(GameObject newSpawnPoint)
    {
        respawnPoint = newSpawnPoint;
    }
}
