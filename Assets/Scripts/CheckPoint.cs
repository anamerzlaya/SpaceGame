using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("check");
            CheckPointsController.instance.SetSpawnPoint(transform.position);
            PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
            HealthBar.instance.SetHealth(PlayerHealthController.instance.currentHealth);
            AudioManager.instance.PlaySFX(5);

        }
    }
}
