using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{

    private Animator theAnimator;
    private Rigidbody2D theRB;

    public Transform groundCheckUp;
    public Transform groundCheckDown;
    public Transform groundCheckRight;
    public LayerMask whatIsGround;
    private bool isGroundedUp;
    private bool isGroundedDown;
    private bool isGroundedRight;

    private bool isMovingUp;
    private bool isMovingRight;

    public enum bossStates {idle, fastidle, aim, shoot};
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

    [Header("ShootPlayer")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBtwShots;
    public float shotCounter;


    //private Vector3 attackTarget;
    //private Vector3 MoveBackPosition;

    // Start is called before the first frame update
    void Start()
    {
        theAnimator = GetComponent<Animator>();
        theRB = GetComponent<Rigidbody2D>();

        idleMoveDirection.Normalize();
        fastIdleMoveDirection.Normalize();

        isMovingUp = true;
        isMovingRight = true;

        currentState = bossStates.idle;  //currentState=0; 
    }

    // Update is called once per frame
    void Update()
    {

        isGroundedUp = Physics2D.OverlapCircle(groundCheckUp.position, .5f, whatIsGround);
        isGroundedDown = Physics2D.OverlapCircle(groundCheckDown.position, .5f, whatIsGround);
        isGroundedRight = Physics2D.OverlapCircle(groundCheckRight.position, .5f, whatIsGround);

        //FastIdleState();
        switch (currentState)
        {
            case bossStates.idle:


                break;


            case bossStates.fastidle:

                break;


            case bossStates.aim:

                break;


            case bossStates.shoot:

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
