using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{

    private Animator theAnimator;

    private Vector3 attackTarget;
    private Vector3 MoveBackPosition;

    // Start is called before the first frame update
    void Start()
    {
        theAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
