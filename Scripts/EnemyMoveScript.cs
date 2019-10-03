using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveScript : MonoBehaviour
{
    private float cDetectAngle, cDetectDist, detectAngle, detectDist;

   // [SerializeField]
  //  private Animator myAnimator;

    private Vector3 mypos, targetpos;

    NavMeshAgent myAgent;

    [SerializeField]
    private bool targetSpotted;

    bool isAlive = true;

    bool attacking = false;

    EnemyAnimController eac;

    private void Awake()
    {
        cDetectAngle = 360f;
        cDetectDist = transform.localScale.x + transform.localScale.x/3;
        detectAngle = 90f;
        detectDist = (transform.localScale.x * 3.5f);
        myAgent = GetComponent<NavMeshAgent>();
        mypos = transform.position;
        targetpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        targetSpotted = false;

        eac = gameObject.GetComponent<EnemyAnimController>();
    }

    private void LateUpdate()
    {
            CheckSight();
    }

    private void CheckSight()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && isAlive)
        {
            mypos = transform.position;
            targetpos = GameObject.FindGameObjectWithTag("Player").transform.position;

            Vector3 direction = targetpos - mypos;
            float angle = Vector3.Angle(direction, transform.forward);
            float dist = Vector3.Distance(mypos, targetpos);

            if (dist < detectDist)
            {
                eac.EnemyMove(true);
                myAgent.SetDestination(targetpos);

                if (dist <= myAgent.stoppingDistance)
                {
                    eac.EnemyMove(false);
                    myAgent.isStopped = true;
                    FaceTarget();
                    if (!eac.IsAttackOnCooldown())
                    eac.StartCoroutine("EnemyAttack");
                }
            }
        }
        
    }

    void FaceTarget()
    {
        Vector3 dir = (targetpos - mypos).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(mypos, cDetectDist);
        Gizmos.DrawWireSphere(mypos, detectDist);
    }
    void changeDeathStatus(bool status)
    {
        isAlive = status;
    }
}
