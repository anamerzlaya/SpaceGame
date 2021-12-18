using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health;
    public Animator animator;
    public GameObject bloodEffect;
    public GameObject deathEffect;

    public GameObject healthPickUp;

    public static EnemyDamage instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("enemy damage taken!");
        if (health > 0)
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(healthPickUp, transform.position, Quaternion.identity);
            
        }
    }

}
