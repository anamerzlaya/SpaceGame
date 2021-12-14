using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;
    //public Animator camAnim;
    //public Animator playerAnim;

    void Start()
    {
        
    }

    void Update()
    {
        if (timeBtwAttack <=0)
        {
            //you can attack
            timeBtwAttack = startTimeBtwAttack;
            if (Input.GetKey(KeyCode.Space))
            {
                //camAnim.SetTrigger("shake");
                //playerAnim.SetTrigger("attack");
                Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemy);
                for(int i = 0; i < enemyToDamage.Length; i++)
                {
                    enemyToDamage[i].GetComponent<EnemyDamage>().TakeDamage(damage);
                }
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