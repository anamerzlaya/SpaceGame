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

    private bool isStageTwo;

    public enum bossStates {initStageOne, initStageTwo, idle, fastidle, aim, shoot};
    public bossStates currentState;

    //idle
    [Header("Idle")]
    public float idleMoveSpeed;
    public Vector2 idleMoveDirection;

    [Header("FastIdle")]
    public float fastIdleSpeed;
    public Vector2 fastIdleMoveDirection;

    [Header("AimPlayer")]
    public float chaseSpeed;
    private Vector3 attackTarget;
    private float attackCounter;

    [Header("ShootPlayer")]
    public GameObject bulletPack;
    public float timeBtwShots;
    private float shotCounter;
    public Transform firePoint;


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

        isStageTwo = false;
        currentState = bossStates.initStageOne;  //currentState=0; 
    }

    // Update is called once per frame
    void Update()
    {
        isGroundedUp = Physics2D.OverlapCircle(groundCheckUp.position, .5f, whatIsGround);
        isGroundedDown = Physics2D.OverlapCircle(groundCheckDown.position, .5f, whatIsGround);
        isGroundedRight = Physics2D.OverlapCircle(groundCheckRight.position, .5f, whatIsGround);

        attackTarget = Vector3.zero;


        if ((EnemyDamageBoss.instance.health < EnemyDamageBoss.instance.healthMax / 2) && !isStageTwo)
        {
            //stage two starts
            isStageTwo = true;
            idleMoveSpeed = idleMoveSpeed * 1.5f;
            fastIdleSpeed = fastIdleSpeed * 1.5f;
            chaseSpeed = chaseSpeed * 1.5f;
            currentState = bossStates.initStageTwo;
        }

        if (attackTarget == Vector3.zero)
        {
            attackTarget = PlayerController.instance.transform.position;
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
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_idle"))
                {
                    currentState = bossStates.idle;
                }
                break;

            case bossStates.idle:
                IdleState();
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_fastidle"))
                {
                    currentState = bossStates.fastidle;
                }
                else if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_aim"))
                {
                    attackTarget = PlayerController.instance.transform.position;
                    currentState = bossStates.aim;

                }
                break;


            case bossStates.fastidle:
                FastIdleState();
                if (theAnimator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBoss_idle"))
                {
                    currentState = bossStates.idle;
                }
                break;


            case bossStates.aim:
                
                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, attackTarget) <= .1f)
                {
                    //just attacked -> go to idle state
                    theAnimator.SetTrigger("idle"); 
                    currentState = bossStates.idle;
                }
                break;


            case bossStates.shoot:

                shotCounter-=Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBtwShots;

                   
                    var newBullet =  Instantiate(bulletPack, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                    //Instantiate(bulletPack, transform.position, transform.rotation);

                }

                break;


        }
    }

    private void IdleState()
    {
        if ((isGroundedUp && isMovingUp) || (isGroundedDown && !isMovingUp))
        {    
            ChangeDirection();
        }
        if (isGroundedRight)
        {
            FlipDirection();
        }

        theRB.velocity = idleMoveSpeed * idleMoveDirection;
    }
   private void FastIdleState()
    {
        if ((isGroundedUp && isMovingUp) || (isGroundedDown && !isMovingUp))
        {    
            ChangeDirection();
        }
        if (isGroundedRight)
        {
            FlipDirection();
        }

        theRB.velocity = fastIdleSpeed * fastIdleMoveDirection;
    }

    private void AimToPlayer()
    {
        attackTarget = PlayerController.instance.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

    }

    private void ChangeDirection()
    {
        isMovingUp = !isMovingUp;
        idleMoveDirection.y *= -1;
        fastIdleMoveDirection.y *= -1;
    }

    private void FlipDirection()
    {
        isMovingRight = !isMovingRight;
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
