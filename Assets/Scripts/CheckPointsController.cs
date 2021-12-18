using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsController : MonoBehaviour
{

    public static CheckPointsController instance;

    public CheckPoint[] checkpoints;

    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<CheckPoint>();
        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    public void SetSpawnPoint(Vector3 newSpawnPos)
    {
        spawnPoint=newSpawnPos;

    }
}
