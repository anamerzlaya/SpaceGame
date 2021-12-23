using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToRespawn;

    public int AttemptsCounter;

    public float playerArenaX, playerArenaY;
    private bool IsArena = false;

    public GameObject Boss;
    public GameObject BossHealth;

    private void Awake()
    {
        instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        AttemptsCounter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((PlayerController.instance.transform.position.x > playerArenaX) && (PlayerController.instance.transform.position.y < playerArenaY))
        {
            //its Boss arena
            IsArena = true;
        }
        if (IsArena)
        {
            Boss.SetActive(true);
            BossHealth.SetActive(true);

        }
    }
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckPointsController.instance.spawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        HealthBar.instance.SetHealth(PlayerHealthController.instance.currentHealth);

        //if (EnemyDamageBoss.instance.gameObject.active)
       // {
            EnemyDamageBoss.instance.health = EnemyDamageBoss.instance.healthMax;
            HealthBarBoss.instance.SetHealth(EnemyDamageBoss.instance.health);
       // }
            

        AttemptsCounter++;
    }
}
