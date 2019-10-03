using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //Calls the animation for character movement
    void MoveAnim(int direction)
    {
        GetComponent<Animator>().SetInteger("MoveDir", direction);
    }
    void MoveSubAnim(int subDirection)
    {
        GetComponent<Animator>().SetInteger("MoveSubDir", subDirection);
    }
    //Plays dead animation when player dies
    void DeadAnim(bool isDead)
    {
        GetComponent<Animator>().SetBool("Dead", isDead);
    }
    //Turn animation
    void TurnAnim(int turnDirection)
    {
        GetComponent<Animator>().SetInteger("Turning", turnDirection);
    }
    //Jump Animation
    void JumpAnim(bool isJumping)
    {
        GetComponent<Animator>().SetBool("IsJumping", isJumping);
    }
    //Landing animation (from a jump)
    void LandingAnim(bool isFalling)
    {
        GetComponent<Animator>().SetBool("JumpPeak", isFalling);
    }
    void AttackAnimSelect(int animSelect)
    {
        GetComponent<Animator>().SetInteger("AttackNum", animSelect);
    }
    void PlayingAttackAnim(bool isAttacking)
    {
        GetComponent<Animator>().SetBool("IsAttacking", isAttacking);
         if (GetComponent<Animator>().GetInteger("WepEquipped") == 2)
        {
            GetComponent<Animator>().SetTrigger("SwordAttack");
        }
     //   Debug.Log("Is Attacking?: " + isAttacking);
    }
    void ChangeWeaponAnim(int wepToChange)
    {
        GetComponent<Animator>().SetInteger("WepEquipped", wepToChange);
        GetComponent<Animator>().SetTrigger("WepSwap");
    }
    void SetToIdle()
    {
        MoveAnim(5);
        MoveSubAnim(5);
        PlayingAttackAnim(false);
        AttackAnimSelect(0);
        JumpAnim(false);
    }
}
