using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    int TrapDamage = 30;
    bool enemyCooldown = false;
    float enemyCooldownDur = 5f;
    bool trapCooldown = false;
    float trapCooldownDur = 2.5f;
    int enemyDamage;

    
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    // Collision Handling for the PlayerModel
  /*  private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag != "Terrain")
        {
            
            // Differentiates trap from other potential collisions.
            if (hit.gameObject.tag == "Trap" || (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<EnemyHealthController>().CheckAliveStatus() == true) && !Cooldown)
            {
                //Sends a message to PlayerController telling it to take damage from trap
                SendMessageUpwards("TakeDamage", TrapDamage);
                //Coroutine for cooldown time
                StartCoroutine("CoolDownRefresh");
                Cooldown = true;
                Debug.Log("Damage");
            }
            if (hit.gameObject.tag == "Respawn")
            {
                SendMessageUpwards("TakeDamage", 9999);
            }
        }*
        if (hit.gameObject.tag == "Enemy")
        {
        /*
            if (PlayerController.currentSkill != null)
            {
            EnemyController.TakeDamage(PlayerController.currentSkill.attackDamage);
            }
       
        }
    }*/
    void OnTriggerStay(Collider other)
    {
    
        if (other.gameObject.tag == "Trap" && !trapCooldown)
        {
            SendMessageUpwards("TakeDamage", TrapDamage);
            trapCooldown = true;
            StartCoroutine("CoolDownRefreshTrap");
        }
        if (other.gameObject.tag == "Enemy" && !enemyCooldown)
        {
            Debug.Log("Hit");
            enemyDamage = CalculateDamage(other.gameObject);
            SendMessageUpwards("TakeDamage", enemyDamage);
            enemyCooldown = true;
            StartCoroutine("CoolDownRefreshEnemy");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            SendMessageUpwards("ChangeRespawnPoint", other.gameObject);
        }
    }


    // cooldown timer
    IEnumerator CoolDownRefreshTrap()
    {
        yield return new WaitForSecondsRealtime(trapCooldownDur);
        trapCooldown = false;
    }
    // cooldown timer
    IEnumerator CoolDownRefreshEnemy()
    {
        yield return new WaitForSecondsRealtime(enemyCooldownDur);
        enemyCooldown = false;
    }

    int CalculateDamage(GameObject enemy)
    {
        int ed;
        if (enemy.GetComponent<EnemyHealthController>().enemyType == 1)
        {
            ed = (int)EnemyHealthController.EnemyDamageAmt.Spider;
        }
        else if (enemy.GetComponent<EnemyHealthController>().enemyType == 2)
        {
            ed = (int)EnemyHealthController.EnemyDamageAmt.Goblin;
        }
        else if (enemy.GetComponent<EnemyHealthController>().enemyType == 3)
        {
            ed = (int)EnemyHealthController.EnemyDamageAmt.Boss;
        }
        else
        {
            ed = 0;
        }
        return ed;
    }

    void ResetTrapCoolDown()
    {
        trapCooldown = false;
    }
}
