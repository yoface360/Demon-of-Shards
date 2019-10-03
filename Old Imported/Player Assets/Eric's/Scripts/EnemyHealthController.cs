using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    float maxHealth;
    float currentHealth;
    bool isAlive = true;
    bool xpAwarded = false;

    [SerializeField]
    float despawnTime = 30f;
    [SerializeField]
    GameObject droppedItemsPouch;
 //   [SerializeField]
 //   GameObject parentObj;
    EnemyAnimController eac;
     public int enemyType;

     public enum EnemyDamageAmt
    {
        Spider = 10,
        Goblin = 15,
        Boss = 50
    }


    public enum xpKillAwards
    {
        InvalidEnemy = 0,
        Spider = 20,
        Goblin = 50,
        Boss = 250

    }

    enum maxHealthAmount
    {
        Spider = 100,
        Goblin  = 100,
        Boss = 500
    }


    // Start is called before the first frame update
    void Awake()
    {
        maxHealth = CalculateMaxHealth();
        currentHealth = maxHealth;
        eac = gameObject.GetComponent<EnemyAnimController>();
        eac.DetermineEnemyType(enemyType);
        eac.DetermineDamageAmount();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            CheckHealth();
        }
        else
        {
            if (!xpAwarded)
            {
                float enemyXpVal = XpToAward();
                GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerStatsManager>().GainExperience(enemyXpVal);
                xpAwarded = true;
                eac.StartCoroutine("EnemyDeath");
                Instantiate(droppedItemsPouch, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                
            }
            
        }
    }

    void CheckHealth()
    {
        if (currentHealth <= 0f)
        {
            isAlive = false;
        }
    }
    public void TakeDamage(float damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;
            Debug.Log(gameObject.name + " Health: " + currentHealth);
        }
    }
    float XpToAward()
    {
        if (enemyType == 1) 
            return (float)xpKillAwards.Spider;
        else if (enemyType == 2)
            return (float)xpKillAwards.Goblin;
        else if (enemyType == 3)
            return (float)xpKillAwards.Boss;

        return (float)xpKillAwards.InvalidEnemy;
    }
    public bool CheckAliveStatus()
    {
        return isAlive;
    }
    float CalculateMaxHealth()
    {
        if (enemyType == 1)
        {
            maxHealth = (float)maxHealthAmount.Spider;
        }
        else if (enemyType == 2)
        {
            maxHealth = (float)maxHealthAmount.Goblin;
        }
        else if (enemyType == 3)
        {
            maxHealth = (float)maxHealthAmount.Boss;
        }
        else
        {
            maxHealth = 0;
        }
        return maxHealth;
    }
    IEnumerator WaitShort()
    {
        yield return new WaitForSeconds(.5f);
    }
}
