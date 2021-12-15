using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraShake()
    {
        anim.SetTrigger("shake");
    }
    public void CameraShakeJump()
    {
        anim.SetTrigger("shake_jump");
    }

}
