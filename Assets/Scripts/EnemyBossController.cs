using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{

    private Animator theAnimator;
    private Rigidbody2D theRB;
    public Transform theBoss;

    public Transform groundCheckUp;
    public Transform groundCheckDown;
    public Transform groundCheckRight;
    public LayerMask whatIsGround;
    private bool isGroundedUp;
    private bool isGroundedDown;
    private bool isGroundedRight;

    private bool isMovingUp;
    private bool isMovingRight;
    private float previousPosX, previousPosY;
    public float arenaLeft, arenaRight, arenaUp, arenaDown;

    private bool isStageOne;
    private bool isStageTwo;



    //public enum bossStates {initStageOne, initStageTwo, idle, fastidle, aim, shoot};
    public enum bossStates {initStageOne, initStageTwo, idle, fastidle, shoot};
    public bossStates currentState;

    //idle
    [Header("Idle")]
    public float idleMoveSpeed_init;
    private float idleMoveSpeed;
    public Vector2 idleMoveDirection;

    [Header("FastIdle")]
    public float fastIdleSpeed_init;
    private float fastIdleSpeed;
    public Vector2 fastIdleMoveDirection;

    [Header("AimPlayer")]
    public float chaseSpeed;
    private Vector3 attackTarget;
    private float attackCounter;

    [Header("ShootPlayer")]
    public GameObject bulletPack;
    public float timeBtwShots;
    private float shotCounter;
    public Transform firePointLeft;
    public Transform firePointRight;
    private bool isBulletCreated;


    //private Vector3 attackTarget;
    //private Vector3 MoveBackPosition;

    public static EnemyBossController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theAnimator = GetComponent<Animator>();
        theRB = GetComponent<Rigidbody2D>();

        idleMoveDirection.Normalize();
        fastIdleMoveDirection.Normalize();

        isMovingUp = true;
        isMovingRight = true;
        previousPosX = transform.position.x;
        previousPosY = transform.position.y;

        isStageOne = false;
        isStageTwo = false;
        idleMoveSpeed = idleMoveSpeed_init;
        fastIdleSpeed = fastIdleSpeed_init;
        currentState = bossStates.initStageOne;  //currentState=0; 

        isBulletCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGroundedUp = Physics2D.OverlapCircle(groundCheckUp.position, .5f, whatIsGround);
        isGroundedDown = Physics2D.OverlapCircle(groundCheckDown.position, .5f, whatIsGround);
        isGroundedRight = Physics2D.OverlapCircle(groundCheckRight.position, .5f, whatIsGround);

        //attackTarget = Vector3.zero;

        if (transform.position.x >= previousPosX)
            isMovingRight = true;
        else isMovingRight = false;
        if (transform.position.y >= previousPosY)
            isMovingUp = true;
        else isMovingUp = false;
        previousPosX = transform.position.x;
        previousPosY = transform.position.y;

        //LimitMovement();

       
        
        float clampedY = Mathf.Clamp(transform.position.y, arenaDown, arenaUp);
        float clampedX = Mathf.Clamp(transform.position.x, arenaLeft, arenaRight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        //if (attackTarget == Vector3.zero)
        //{
        //    attackTarget = PlayerController.instance.transform.position;
        //}
        if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_idle"))
        {
            currentState = bossStates.idle;
        }
        if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_fastidle"))
        {
            currentState = bossStates.fastidle;
        }
        if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_idle"))
        {
            currentState = bossStates.idle;
        }
        if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_fastidle"))
        {
            currentState = bossStates.fastidle;
        }
        
        if ( (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_Shoot")) || (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_Shoot")))
        {
            currentState = bossStates.shoot;
        }

        if ((EnemyDamageBoss.instance.health == EnemyDamageBoss.instance.healthMax ) && isStageTwo)
        {
            //stage one starts
            isStageTwo = false;
            idleMoveSpeed = idleMoveSpeed_init;
            fastIdleSpeed = fastIdleSpeed_init;
            chaseSpeed = chaseSpeed * 1.2f;
            currentState = bossStates.initStageOne;
        }
        if ((EnemyDamageBoss.instance.health < EnemyDamageBoss.instance.healthMax / 2) && !isStageTwo)
        {
            //stage two starts
            //isStageOne = false;
            isStageTwo = true;
            idleMoveSpeed = idleMoveSpeed_init * 1.3f;
            fastIdleSpeed = fastIdleSpeed_init * 1.3f;
            chaseSpeed = chaseSpeed * 1.2f;
            currentState = bossStates.initStageTwo;
        }
        switch (currentState)
        {
            case bossStates.initStageOne:
                //Debug.Log("Boss: init stage one");
                theAnimator.SetTrigger("stageOne");
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_idle"))
                {
                    currentState = bossStates.idle;
                }
                isBulletCreated = false;

                break;

            case bossStates.initStageTwo:
                //Debug.Log("Boss: init stage two");
                theRB.velocity = new Vector2(0f, 0f);
                theAnimator.SetTrigger("stageTwo");
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_idle"))
                {
                    currentState = bossStates.idle;
                }
                isBulletCreated = false;

                break;

            case bossStates.idle:
            /*    if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_fastidle"))
                {
                    currentState = bossStates.fastidle;
                }
                else if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_aim"))
                {
                    currentState = bossStates.aim;
                    theAnimator.SetTrigger("aim");

                }
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_fastidle"))
                {
                    currentState = bossStates.fastidle;
                }
                else if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_aim"))
                {
                    currentState = bossStates.aim;
                    theAnimator.SetTrigger("aim");
                }
             */   
                isBulletCreated = false;
                IdleState();

                break;


            case bossStates.fastidle:
               /* if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_idle"))
                {
                    currentState = bossStates.idle;
                }
                else if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_idle"))
                {
                    currentState = bossStates.idle;
                }
                else 
                 */   
                FastIdleState();
                isBulletCreated = false;

                break;


            
            case bossStates.shoot:
                //FlipTowardsPlayer();
                //Debug.Log("is bullet?" + isBulletCreated);
                theRB.velocity = new Vector2(0f, 0f);
                //Debug.Log(theRB.velocity.x + theRB.velocity.y);
                if (!isBulletCreated)
                {
                    isBulletCreated = true;
                    Debug.Log("BulletCreated bullet");
                    var newBulletLeft = Instantiate(bulletPack, firePointLeft.position, firePointLeft.rotation);
                    newBulletLeft.transform.localScale = theBoss.localScale;
                   var newBulletRight = Instantiate(bulletPack, firePointRight.position, firePointRight.rotation);
                    newBulletRight.transform.localScale = theBoss.localScale;
                }
                theAnimator.SetTrigger("idle");
                currentState = bossStates.idle;

                //LimitMovement();

                break;


        }        
        
        
        /*
        isGroundedUp = Physics2D.OverlapCircle(groundCheckUp.position, .5f, whatIsGround);
        isGroundedDown = Physics2D.OverlapCircle(groundCheckDown.position, .5f, whatIsGround);
        isGroundedRight = Physics2D.OverlapCircle(groundCheckRight.position, .5f, whatIsGround);

        //attackTarget = Vector3.zero;

        if (transform.position.x >= previousPosX)
            isMovingRight = true;
        else isMovingRight = false;
        if (transform.position.y >= previousPosY)
            isMovingUp = true;
        else isMovingUp = false;
        previousPosX = transform.position.x;
        previousPosY = transform.position.y;

        //LimitMovement();

        if ((EnemyDamageBoss.instance.health < EnemyDamageBoss.instance.healthMax / 2) && !isStageTwo)
        {
            //stage two starts
            isStageTwo = true;
            idleMoveSpeed = idleMoveSpeed * 1.2f;
            fastIdleSpeed = fastIdleSpeed * 1.2f;
            chaseSpeed = chaseSpeed * 1.2f;
            currentState = bossStates.initStageTwo;
        }
        float clampedY = Mathf.Clamp(transform.position.y, arenaDown, arenaUp);
        float clampedX = Mathf.Clamp(transform.position.x, arenaLeft, arenaRight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        //if (attackTarget == Vector3.zero)
        //{
        //    attackTarget = PlayerController.instance.transform.position;
        //}
        if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_idle"))
        {
            currentState = bossStates.idle;
        }
        if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_fastidle"))
        {
            currentState = bossStates.fastidle;
        }
        if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_aim"))
        {
            currentState = bossStates.aim;
        }

            switch (currentState)
        {
            case bossStates.initStageOne:
                //Debug.Log("Boss: init stage one");
                theAnimator.SetTrigger("stageOne");
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_idle"))
                {
                    currentState = bossStates.idle;
                }

                break;

            case bossStates.initStageTwo:
                //Debug.Log("Boss: init stage two");
                theAnimator.SetTrigger("stageTwo");
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_idle"))
                {
                    currentState = bossStates.idle;
                }

                break;

            case bossStates.idle:
            /*    if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_fastidle"))
                {
                    currentState = bossStates.fastidle;
                }
                else if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_aim"))
                {
                    currentState = bossStates.aim;
                    theAnimator.SetTrigger("aim");

                }
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_fastidle"))
                {
                    currentState = bossStates.fastidle;
                }
                else if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_aim"))
                {
                    currentState = bossStates.aim;
                    theAnimator.SetTrigger("aim");
                }
              * /  isBulletCreated = false;
                attackTarget = PlayerController.instance.transform.position;
                IdleState();

                break;


            case bossStates.fastidle:
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_idle"))
                {
                    currentState = bossStates.idle;
                }
                else if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_angry_idle"))
                {
                    currentState = bossStates.idle;
                }
                else FastIdleState();

                break;


            case bossStates.aim:

                //if (isGroundedDown || isGroundedUp || isGroundedRight)
                //{
                //    if (isGroundedDown || isGroundedUp)
                //   {
                //        transform.position
                 //   }
                //}
                //else
                //{
                 if (attackTarget != Vector3.zero)
                 {
                        //float clampedY = Mathf.Clamp(attackTarget.y, arenaDown, arenaUp);
                        //float clampedX = Mathf.Clamp(attackTarget.x, arenaLeft, arenaRight);
                    //if ((transform.position.x >=arenaLeft && transform.position.x <=arenaRight) && (transform.position.y>=arenaDown && transform.position.y <= arenaUp))
                    //{
                            transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);
                    //}
                    //else
                    //{
                    //    Debug.Log("boss outside of arena->go to idle");
                    //    //currentState = bossStates.idle;
                    //    theAnimator.SetTrigger("idle");
                    //}
                }
                else if(attackTarget == Vector3.zero)
                {
                    //Debug.Log(attackTarget.x + attackTarget.y);
                   // Debug.Log("no aiming target -> go to idle");
                        currentState = bossStates.idle;
                        theAnimator.SetTrigger("idle");
                }
                    if (Vector3.Distance(transform.position, attackTarget) <= 1f)
                    {
                    //just attacked -> go to idle state
                    Debug.Log("just attacked - > go to idle and set target to zero");
                    ChangeDirection();

                    theAnimator.SetTrigger("idle");
                        currentState = bossStates.idle;
                        attackTarget = Vector3.zero;
                    }
                    //LimitMovement();
                //}
                break;


            case bossStates.shoot:
                

                if (!isBulletCreated)
                {
                    isBulletCreated = true;
                    Debug.Log("BulletCreated bullet");
                    var newBullet = Instantiate(bulletPack, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }
                theAnimator.SetTrigger("idle");
                currentState = bossStates.idle;

                //LimitMovement();

                break;


        }
        */
    }

    private void LimitMovement()
    {
        //if ((isGroundedUp && isMovingUp) || (isGroundedDown && !isMovingUp))
        if ((isGroundedUp ) || (isGroundedDown ))
        {
            ChangeDirection();
        }
        if (isGroundedRight)
        {
            FlipDirection();
        }
    }

    private void IdleState()
    {
        LimitMovement();

        theRB.velocity = idleMoveSpeed * idleMoveDirection;
    }
   private void FastIdleState()
    {
        LimitMovement();

        theRB.velocity = fastIdleSpeed * fastIdleMoveDirection;
    }

    private void AimToPlayer()
    {
        attackTarget = PlayerController.instance.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

    }

    private void ChangeDirection()
    {
        //isMovingUp = !isMovingUp;
        idleMoveDirection.y *= -1;
        fastIdleMoveDirection.y *= -1;
    }

    private void FlipDirection()
    {
        //isMovingRight = !isMovingRight;
        idleMoveDirection.x *= -1;
        fastIdleMoveDirection.x *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    private void FlipTowardsPlayer()
    {
        if ( ((attackTarget.x - transform.position.x > 0) && !isMovingRight) || ((attackTarget.x - transform.position.x < 0 ) && isMovingRight) )
        {
            FlipDirection();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, .5f);
        Gizmos.DrawWireSphere(groundCheckDown.position, .5f);
        Gizmos.DrawWireSphere(groundCheckRight.position, .5f);
    }

}
