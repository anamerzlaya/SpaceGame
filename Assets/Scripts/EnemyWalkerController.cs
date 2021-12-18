using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkerController : MonoBehaviour
{

    private float moveSpeed;
    public float moveSpeedInit;

    public float moveRange;
    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D theRB;
    //public SpriteRenderer theSR;

    //public float moveTime, waitTime;
    //private float moveCount, waitCount;

    private float dazedTime;
    public float startDazedTime;
    private int currentHealth;
    private int previousHealth;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        leftPoint.parent = null;
        rightPoint.parent = null;
        movingRight = true;

        moveSpeed = moveSpeedInit;

        //moveCount = moveTime;
        //waitCount = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (dazedTime <= 0)
        {
            moveSpeed = moveSpeedInit;
        }
        else
        {
            moveSpeed = 0;
            dazedTime-=Time.deltaTime;
        }
        currentHealth = EnemyDamage.instance.health; 
        if (currentHealth!= previousHealth)//enemy was just damaged
        {
            dazedTime = startDazedTime;
        }
        previousHealth = currentHealth;

 //       if (moveCount > 0)
 //       {
 //           moveCount -= Time.deltaTime;
            if (movingRight)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                Vector3 rotationVector = new Vector3(0f, 0f, 0f);
                transform.rotation = Quaternion.Euler(rotationVector);
                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                Vector3 rotationVector = new Vector3(0f, 180f, 0f);
                transform.rotation = Quaternion.Euler(rotationVector);
                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }
        /*     if (moveCount <= 0)
          {
              waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
          }
          }
             else if (waitCount > 0)
             {
                 waitCount -= Time.deltaTime;
                 theRB.velocity = new Vector2(0f, theRB.velocity.y);
                 if (waitCount <= 0)
                 {
                     moveCount = Random.Range(moveTime * .75f, moveTime * 1.25f);
                 }
             }

     */
    }
}
