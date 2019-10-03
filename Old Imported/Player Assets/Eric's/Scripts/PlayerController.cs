using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 /*   [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;*/

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject sword;

    //Player's Health and Stamina
    //float playerHealth;
    //float playerStamina;
    //Vec3s for player movement
    Vector3 movement = Vector3.zero;
    Vector3 hor = Vector3.zero;
    Vector3 ver = Vector3.zero;

    //Checks if player is dead, and the Timers for respawning them
    bool isAlive = true;

    bool canAttack = true;


    //Attacking vars, first one is currently unused.
    float currentAttackCooldownDur;
    bool attackOnCooldown = false;
    int currentWeapon = 1;
    bool currentlyAttacking = false;

    //Jumping code vars
    Vector3 jumpDirection;
    // Is the character in the jumping state?
    bool isJumping = false;
    //Has the character reached the peak of the jump?
    bool hasReachedPeak = false;
    //Is the character near the ground?
    bool nearGround = false;




    //CharacterController variable, used for movement
    CharacterController cController;

    //Current Skill, public for easier access
    public Skill currentSkill;
    //checks to see if the player is grounded 
    bool grounded;
    //height of char
    float distanceToGround = 1.35f;
    //distance from ground as to when to play animation
    float nearDist = .025f;

    bool hasHit = false;

    float time = 0.0f;



    //[SerializeField]
    //HealthManaManager barManager;

    //Coefficient for gravity
    [SerializeField]
    float gravity = 20.0f;

    //How high to jump
    [SerializeField]
    float jumpHeight = 8.0f;

    //Speed of Character
    [SerializeField]
    float speed = 3.0F;


    /// <summary>
    /// Weapon Attacking position List, for handling collisions against enemies
    /// </summary>
    [SerializeField]
    List<Transform> weaponAttackPositionList = new List<Transform>(2);

    //Speed to rotate character, commented for Archival purposes
    
    [SerializeField]
    float rotateSpeed = 180.0F;
    

    //Current player in gamespace
    [SerializeField]
    GameObject player;

    [SerializeField]
    Transform camera;

    //Enumerated type for which cardinal direction to traverse
    enum Direction
    {
        //Apparently using 0 for the Broadcast functions doesnt work for some odd reason
        Forward = 1,
        Backward = 2,
        Right = 3,
        Left = 4,
        Idle = 5

    }

    //Enumerated type for which sub-cardinal direction to traverse
    enum SubDirection
    {
        Left = -1,
        Right = 1,
        Idle = 5
    }
    //Enumerated Type for which direction to turn
        enum TurnDirection
    {
        CounterClock = -1,
        Idle = 5,
        Clock = 1
    }
    //Enumerated Type for current weapons in game (Only unarmed is implemented for now)
    public enum Weapons
    {
        Unarmed = 1,
        Sword = 2,
        Spear = 3,
        Axe = 4,
        Mace = 5,
        Hammer = 6,
        Bow = 7
        
    }

 /*   void Awake()
    {
        health.Initialize();
        mana.Initialize();
    }*/

    void Start()
    {
        //uses character controller to move player, initializes health and stamina
        //playerHealth = 100;
        //playerStamina = 100;
        //barManager.changeHealth(playerHealth);
      //  health.MaxValue = 100;
      //  mana.MaxValue = 100;
      //  health.CurrentValue = 100;
      //  mana.CurrentValue = 100;
        cController = player.GetComponent<CharacterController>();
        
        currentSkill = new Skill();
        sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            //Old Turning code, Commented for Archival Purposes
            /*
                if (Input.GetAxis("Rotate") !=0)
                {
                    if (Input.GetAxis("Rotate") > 0)
                    {
                    // Rotate around y - axis clockwise
                    player.transform.Rotate(0, Input.GetAxis("Rotate") * rotateSpeed, 0);
                        BroadcastMessage("TurnAnim", (int)TurnDirection.Clock);
                    }
                    if (Input.GetAxis("Rotate") < 0)
                    {
                        // Rotate around y - axis counter-clockwise
                        player.transform.Rotate(0, Input.GetAxis("Rotate") * rotateSpeed, 0);
                        BroadcastMessage("TurnAnim", (int)TurnDirection.CounterClock);
                    }

                }
                */

            //New turning code, works better with new movement code
      /*      if (Input.GetAxis("Rotate") != 0)
            {
                if (Input.GetAxis("Rotate") > 0)
                {
                    transform.RotateAround(player.transform.position, Vector3.up, 90f * Mathf.Deg2Rad);
                    BroadcastMessage("TurnAnim", (int)TurnDirection.Clock);
                    camera.transform.position.
                }
                if (Input.GetAxis("Rotate") < 0)
                {
                    transform.RotateAround(player.transform.position, Vector3.down, 90f * Mathf.Deg2Rad);
                    BroadcastMessage("TurnAnim", (int)TurnDirection.CounterClock);
                }
            }
            else
            {
                BroadcastMessage("TurnAnim", (int)TurnDirection.Idle);
            }*/

            //Massively Restructured, now works overall a lot better than before.
            movement = Vector3.zero;
            hor = Vector3.zero;
            ver = Vector3.zero;
            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    ver = camera.transform.forward * speed * 1.5f * Time.deltaTime;
                }
                else if (Input.GetAxis("Vertical") < 0)
                {
                    ver = -camera.transform.forward * speed * 1.5f * Time.deltaTime;
                }
                if (Input.GetAxis("Horizontal") > 0)
                {
                    hor = camera.transform.right * speed * Time.deltaTime;
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    hor = -camera.transform.right * speed * Time.deltaTime;
                }
                movement = hor + ver;
               
                movement.y = 0f;
                cController.Move(movement);
                //player.transform.rotation = Quaternion.LookRotation(movement);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(movement), time);
                /*if (time > 1)
                {
                    time = 0.0f;
                }
                else
                {*/
                    time = time + Time.deltaTime;
               // }
            }

            //Calls the Idle animation when not moving
            if (movement.Equals(Vector3.zero))
            {
                BroadcastMessage("MoveAnim", (int)Direction.Idle);
                BroadcastMessage("MoveSubAnim", (int)SubDirection.Idle);
            }
            else
            {
                BroadcastMessage("MoveAnim", (int)Direction.Forward);
            }

            //Kills player
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                gameObject.GetComponent<PlayerStatsManager>().TakeDamage(500f);
            }

            //Player takes 10 damage
            if (Input.GetKeyDown(KeyCode.L))
            {
                gameObject.GetComponent<PlayerStatsManager>().TakeDamage(10f);
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                jumpDirection.y = jumpHeight;
                BroadcastMessage("LandingAnim", false);
                BroadcastMessage("JumpAnim", true);
                isJumping = true;
            }
            jumpDirection.y -= gravity * Time.deltaTime;
            cController.Move(jumpDirection * Time.deltaTime);


            //Attacking
            if ((Input.GetMouseButtonDown(0)) && !attackOnCooldown && canAttack)
            {
                if (currentWeapon == (int)Weapons.Unarmed)
                {
                    currentSkill = Skill.UseSkill("Punch");
                }
                else if (currentWeapon == (int)Weapons.Sword)
                {
                    currentSkill = Skill.UseSkill("Sword Slash");
                    Debug.Log(currentSkill.SkillName);
                }
                StartCoroutine("Attack");

            }
            else if (Input.GetMouseButtonDown(1) && !attackOnCooldown && canAttack)
            {
                if (currentWeapon == (int)Weapons.Unarmed)
                { 
                currentSkill = Skill.UseSkill("Heavy Punch");
                }
                else if (currentWeapon == (int)Weapons.Sword)
                {
                    currentSkill = Skill.UseSkill("Double Slash");
                    Debug.Log(currentSkill.SkillName);
                }
                StartCoroutine(Attack());

            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (currentWeapon == (int)Weapons.Sword)
                {
                    ChangeEquippedWeapon((int)Weapons.Unarmed);
                }
                else if (currentWeapon == (int)Weapons.Unarmed)
                {
                    ChangeEquippedWeapon((int)Weapons.Sword);
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Used Entirely for jumping & attacking
    /// </summary>
    private void FixedUpdate()
    {
        if (isAlive)
        {
            grounded = isGrounded();
            nearGround = isNearGround();
            //Debug.Log("Grounded?: " + isGrounded());
            if (isJumping)
            {
                CheckJumpHeight();
               // Debug.Log("Near Ground?: " + isNearGround()); test
                // Debug.Log(hasReachedPeak); test
                if (hasReachedPeak && grounded)
                {
                    isJumping = false;
                }
                if (hasReachedPeak && isNearGround())
                {
                    BroadcastMessage("LandingAnim", true);
                   // Debug.Log(player.transform.position.y); test
                }
            }
            if (grounded && !isJumping)
            {
                BroadcastMessage("JumpAnim", false);
            }
            if (currentlyAttacking)
            {
                CheckForHitDetection();
            }
        }
    }
    

    //Coroutine for Attacking with whatever weapon is currently equipped, uses hard values for cooldowns currently.
    IEnumerator Attack()
    {
        //Throws a punch, plays from two different animations
        if (currentWeapon == (int)Weapons.Unarmed)
        {
            //Put skill-Specific code here (not fully implemented)

            int animNum = Random.Range(1, 3);
            attackOnCooldown = true;
            currentlyAttacking = true;
            BroadcastMessage("PlayingAttackAnim", currentlyAttacking);
            BroadcastMessage("AttackAnimSelect", animNum);

            yield return new WaitForSecondsRealtime(currentSkill.AnimDuration);
            currentlyAttacking = false;
            BroadcastMessage("PlayingAttackAnim", currentlyAttacking);
            animNum = 0;
            yield return new WaitForSecondsRealtime(currentSkill.CdDuration);
            attackOnCooldown = false;
            currentSkill = null;
            hasHit = false;
        }

        //Sword not currently implemented
        if (currentWeapon == (int)Weapons.Sword)
        {
            int animNum = Random.Range(1, 6);
            attackOnCooldown = true;
            currentlyAttacking = true;
            BroadcastMessage("PlayingAttackAnim", currentlyAttacking);
            BroadcastMessage("AttackAnimSelect", animNum);

            yield return new WaitForSecondsRealtime(currentSkill.AnimDuration);
            currentlyAttacking = false;
            BroadcastMessage("PlayingAttackAnim", currentlyAttacking);
            animNum = 0;
            yield return new WaitForSecondsRealtime(currentSkill.CdDuration);
            attackOnCooldown = false;
            currentSkill = null;
            hasHit = false;
        }
        if (currentWeapon == (int)Weapons.Spear)
        {

        }
        if (currentWeapon == (int)Weapons.Axe)
        {

        }
        if (currentWeapon == (int)Weapons.Mace)
        {

        }
        if (currentWeapon == (int)Weapons.Hammer)
        {

        }
        if (currentWeapon == (int)Weapons.Bow)
        {

        }
    }
    /// <summary>
    /// Checks to see if the player character is grounded via Raycasts
    /// </summary>
    /// <returns></returns>
    bool isGrounded()
    {
        return Physics.Raycast(player.transform.position, Vector3.down, distanceToGround);
    }
    /// <summary>
    /// Checks the current jump height, and if it should progress the jump into the next stage
    /// </summary>
    void CheckJumpHeight()
    {
        
        if (!Physics.Raycast(player.transform.position, Vector3.down,(jumpHeight /  (gravity * 2)) + distanceToGround))
            {
            hasReachedPeak = true;
            //Debug.Log(player.transform.position.y); test
        }
    }

    /// <summary>
    /// Checks to see if the player is near the ground, to play the land animation
    /// </summary>
    /// <returns></returns>
    bool isNearGround()
    {
        return Physics.Raycast(player.transform.position, Vector3.down, distanceToGround - nearDist);

        
    }
    void CheckForHitDetection()
    {
        RaycastHit hit;
       /* Debug.Log(currentWeapon == (int)Weapons.Unarmed);
        Debug.Log(currentlyAttacking);
        Debug.Log(hasHit);*/
        if (currentWeapon == (int)Weapons.Unarmed && currentlyAttacking && !hasHit)
        {
           // Debug.Log("Has hit?: " + Physics.Raycast(weaponAttackPositionList[0].position, -weaponAttackPositionList[0].right, 3f));
            if (Physics.Raycast(weaponAttackPositionList[0].position, -weaponAttackPositionList[0].right, out hit, 3f))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    GameObject e = hit.collider.gameObject;
                    bool isBoss = false;
                    if (e.GetComponent<HealthEnemyController>() != null)
                    {
                        isBoss = true;
                    }
                    hasHit = true;
                    if (!isBoss)
                        e.GetComponent<EnemyHealthController>().TakeDamage(currentSkill.Damage);
                    else if (isBoss)
                        e.GetComponent<HealthEnemyController>().TakeDamage((int)currentSkill.Damage);

                }
            }
            
        }
        else if (currentWeapon == (int)Weapons.Sword && currentlyAttacking && !hasHit)
        {
            // Debug.Log("Has hit?: " + Physics.Raycast(weaponAttackPositionList[0].position, -weaponAttackPositionList[0].right, 3f));
            
            if (Physics.Raycast(weaponAttackPositionList[1].position, -weaponAttackPositionList[1].right, out hit, 5f))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    GameObject e = hit.collider.gameObject;
                    hasHit = true;
                    bool isBoss = false;
                    if (e.GetComponent<HealthEnemyController>() != null)
                    {
                        isBoss = true;
                    }
                    hasHit = true;
                    if (!isBoss)
                        e.GetComponent<EnemyHealthController>().TakeDamage(currentSkill.Damage);
                    else if (isBoss)
                        e.GetComponent<HealthEnemyController>().TakeDamage((int)currentSkill.Damage);
                }
            }
        }
    }
    void ChangeEquippedWeapon(int weaponToEquip)
    {
        currentWeapon = weaponToEquip;
        BroadcastMessage("ChangeWeaponAnim", weaponToEquip);
        StartCoroutine("DespawnSword");

    }
    void IsPlayerAlive(bool value)
    {
        isAlive = value;
    }
    void ToggleAttacks(bool togAtk)
    {
        canAttack = togAtk;
    }
    IEnumerator DespawnSword()
    {
        yield return new WaitForSecondsRealtime(.35f);
        if (currentWeapon == 1)
        {
            sword.SetActive(false);
        }
        else if (currentWeapon == 2)
        {
            sword.SetActive(true);
        }

    }
}
