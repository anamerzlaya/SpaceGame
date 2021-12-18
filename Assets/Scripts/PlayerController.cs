using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float moveSpeed;
    public float moveSpeedInit;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    public Rigidbody2D theRB;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool isGrounded_tmp;
    private bool stoppedJumping;

    private SpriteRenderer theSR;
    private Animator theAnimator;
    private Collider2D theCollider;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    //public Animator camAnim;


    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        theAnimator = GetComponent<Animator>();
        theCollider = GetComponent<Collider2D>();

        jumpTimeCounter = jumpTime;
        stoppedJumping = true;

        isGrounded_tmp = isGrounded;

        moveSpeed = moveSpeedInit;    

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerAttackController.instance.timeBtwAttack <=0 && (Input.GetKeyDown(KeyCode.Return))) //don't move during atack
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = moveSpeedInit;
        }


            if (knockBackCounter <= 0)
        {

            theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    stoppedJumping = false;
                    theAnimator.SetTrigger("takeOf");

                }
            }
            if (Input.GetKey(KeyCode.Space) && !stoppedJumping)
            {
                if (jumpTimeCounter>0)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    jumpTimeCounter-=Time.deltaTime;
                
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpTimeCounter = 0;
                stoppedJumping = true;

            }
            if (isGrounded)
            {
                jumpTimeCounter = jumpTime;

            }

                /* if ((isGrounded_tmp!=isGrounded) && isGrounded)
                {
                    Debug.Log("just landed");
                    camAnim.SetTrigger("shake_jump");
                }
                isGrounded_tmp = isGrounded;
               */
                if (theRB.velocity.x < 0)
                {
                    Vector3 rotationVector = new Vector3(0f, 180f, 0f);
                    transform.rotation = Quaternion.Euler(rotationVector);
                }
                else if (theRB.velocity.x > 0)
                {
                    Vector3 rotationVector = new Vector3(0f, 0f, 0f);
                    transform.rotation = Quaternion.Euler(rotationVector);

                }


        }
        else
        {
            knockBackCounter -=Time.deltaTime;
            if (transform.rotation.y == 0)
            {
                theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
            }
            else if(transform.rotation.y == 180)
            {
                theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
            }
        }

        theAnimator.SetFloat("Speed", theRB.velocity.x * theRB.velocity.x);
        theAnimator.SetBool("Grounded", isGrounded);


    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
    }
}
