using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health;
    public Animator animator;
    public GameObject bloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        health -= damage;
        Debug.Log("enemy damage taken!");
    }
   
}
