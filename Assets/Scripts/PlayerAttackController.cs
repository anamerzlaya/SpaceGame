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
                Debug.Log("enter pressed");
                camAnim.SetTrigger("shake");
                theAnimator.SetTrigger("attack");
                 Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemy);
                 for(int i = 0; i < enemyToDamage.Length; i++)
                {
                    enemyToDamage[i].GetComponent<EnemyDamage>().TakeDamage(damage);
                 }
                
                timeBtwAttack = startTimeBtwAttack;

            }

        }
        else
        {
            timeBtwAttack-= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
