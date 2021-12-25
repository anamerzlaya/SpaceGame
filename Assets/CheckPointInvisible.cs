using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointInvisible : MonoBehaviour
{
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
            CheckPointsController.instance.SetSpawnPoint(transform.position);
            PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
            HealthBar.instance.SetHealth(PlayerHealthController.instance.currentHealth);

        }
    }
}
