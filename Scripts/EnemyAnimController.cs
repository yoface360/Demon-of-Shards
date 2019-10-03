using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    int enemyType;
    Animator myAnim;
    [SerializeField]
    float[] goblinAnimClips = new float[4];
    PlayerStatsManager psm;
    float damageAmt;
    bool attackOnCooldown = false;
    // Start is called before the first frame update
    void Awake()
    {
        myAnim = gameObject.GetComponent<Animator>();
        psm = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetermineEnemyType(int incomingType)
    {
        enemyType = incomingType;
    }
    public void DetermineDamageAmount()
    {
        if (enemyType == 2)
        {
            damageAmt = (float)EnemyHealthController.EnemyDamageAmt.Goblin;
        }
    }

    public IEnumerator EnemyDeath()
    {
        if (enemyType == 2)
        {
            myAnim.SetBool("IsDead", true);
            yield return new WaitForSecondsRealtime(goblinAnimClips[3]);
        }
    }
    public void EnemyMove(bool moveStatus)
    {
        if (enemyType == 2)
        {
            myAnim.SetBool("IsMoving", moveStatus);
        }
    }
    public IEnumerator EnemyAttack()
    {
        if (enemyType == 2)
        {
            myAnim.SetBool("IsAttacking", true);
            attackOnCooldown = true;
            yield return new WaitForSecondsRealtime(goblinAnimClips[1]);
            psm.TakeDamage(damageAmt);
            myAnim.SetBool("IsAttacking", false);
            yield return new WaitForSecondsRealtime(goblinAnimClips[2]);
            attackOnCooldown = false;
        }
    }
    public bool IsAttackOnCooldown()
    {
        return attackOnCooldown;
    }
}
