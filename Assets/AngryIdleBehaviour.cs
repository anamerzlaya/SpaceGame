using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryIdleBehaviour : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;

    private int rand;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);

        rand = Random.Range(0, 3);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer < 0)
        {
            if (rand == 0) animator.SetTrigger("fastIdle");
            else if (rand == 1) animator.SetTrigger("aim");
            else if (rand == 2) animator.SetTrigger("shoot");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
