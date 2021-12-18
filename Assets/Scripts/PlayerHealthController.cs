using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    //public HealthBar healthBar;

    public float invincibleLength;
    private float invincibleCounter;

    public SpriteRenderer theSR;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.instance.SetMaxHealth(maxHealth);
        //theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter-=Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, 1.0f, theSR.color.a);
            }
        }
    }

    public void DealDamage(int damage)
    {
        if (invincibleCounter <= 0){
            currentHealth -= damage;
            HealthBar.instance.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, 0.6f, theSR.color.a);

                PlayerController.instance.KnockBack();
            }
        }
        
    }

    public void HealPlayer(int heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += heal;
        }
    }
}
