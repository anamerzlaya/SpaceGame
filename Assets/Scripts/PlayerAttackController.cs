using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;
    public Animator camAnim;
    public Animator theAnimator;

    public static PlayerAttackController instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        theAnimator = GetComponent<Animator>();
        timeBtwAttack = 0;
    }

    void Update()
    {
        if (timeBtwAttack <=0)
        {
            //you can attack
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //Debug.Log("enter pressed");
                theAnimator.SetBool("attack", true);


                 Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemy);
                 for(int i = 0; i < enemyToDamage.Length; i++)
                 {
                    if ((enemyToDamage[i] is UnityEngine.Object) && (enemyToDamage[i] != null) && enemyToDamage[i].GetComponent<EnemyDamage>())  
                        enemyToDamage[i].GetComponent<EnemyDamage>().TakeDamage(damage);
                    if ((enemyToDamage[i] is UnityEngine.Object) && (enemyToDamage[i] != null) && enemyToDamage[i].GetComponent<EnemyDamageBoss>())  
                        enemyToDamage[i].GetComponent<EnemyDamageBoss>().TakeDamage(damage);
                 }
                
                timeBtwAttack = startTimeBtwAttack;

            }
            else
            {
                theAnimator.SetBool("attack", false);

            }

        }
        else
        {
            timeBtwAttack-= Time.deltaTime;
            theAnimator.SetBool("attack", false);

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void ShakeCamera()
    {
        camAnim.SetTrigger("shake");

    }
}
