using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageBoss : MonoBehaviour
{

    public Slider healthBar;

    public int health;
    public int healthMax;
    public Animator animator;
    public GameObject bloodEffect;
    public GameObject deathEffect;

    public static EnemyDamageBoss instance;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = healthMax;
        HealthBarBoss.instance.SetMaxHealth(healthMax);


    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;

        if (health <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        HealthBarBoss.instance.SetHealth(health);

        Debug.Log("enemy damage taken!");
        if (health > 0)
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        }
    }
}
