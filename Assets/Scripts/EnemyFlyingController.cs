using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour

{
    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;

    public float distanceToAttackPlayer;
    public float chaseSpeed;

    private Animator theAnimator;

    private Vector3 attackTarget;
    private Vector3 MoveUpPosition;

    public float waitAfterAttack;
    private float attackCounter;

    //private Rigidbody2D theRB;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<points.Length; i++)
        {
            points[i].parent = null;
        }
        theAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0) //wait between attacks
        {
            attackCounter -= Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, MoveUpPosition, moveSpeed * Time.deltaTime);
        }
        else
        {

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
            {
                //fly around

                attackTarget = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.5f)
                {
                    currentPoint++;
                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }

                theAnimator.SetBool("aim", false);

            }
            else
            {
                //attack Player

                if (attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);
                theAnimator.SetBool("aim", true);

                if (Vector3.Distance(transform.position, attackTarget) <= .1f)
                {
                    //just attacked
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                    MoveUpPosition = new Vector3(transform.position.x, transform.position.y + distanceToAttackPlayer, transform.position.z);
                }

            }

            if (transform.position.x < points[currentPoint].position.x)
            {
                Vector3 rotationVector = new Vector3(0f, 0f, 0f);
                transform.rotation = Quaternion.Euler(rotationVector);
            }
            else
            {
                Vector3 rotationVector = new Vector3(0f, 180f, 0f);
                transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

    }
}
