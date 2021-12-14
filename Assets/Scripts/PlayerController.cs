using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    public Rigidbody2D theRB;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool stoppedJumping;

    private SpriteRenderer theSR;
    private Animator theAnimator;
    private Collider2D theCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        theAnimator = GetComponent<Animator>();
        theCollider = GetComponent<Collider2D>();

        jumpTimeCounter = jumpTime;
        stoppedJumping = true;

    }

    // Update is called once per frame
    void Update()
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
       

        theAnimator.SetFloat("Speed", theRB.velocity.x* theRB.velocity.x);
       theAnimator.SetBool("Grounded",isGrounded);

        if (theRB.velocity.x < 0)
        {
            Vector3 rotationVector = new Vector3(0, 180, 0);
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        else if (theRB.velocity.x > 0)
        {
            Vector3 rotationVector = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(rotationVector);

        }

    }
}
