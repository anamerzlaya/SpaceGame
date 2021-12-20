using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed;
    public Transform[] bullets;
    private Vector2[] moveDirection;


    //private Vector3 shootTarget;

    void Awake()
    {
        moveDirection = new Vector2[bullets.Length];
    }


    // Start is called before the first frame update
    void Start()
    {
        // shootTarget = PlayerController.instance.transform.position;
        moveDirection[0] = new Vector2(3f, 2f);
        moveDirection[1] = new Vector2(3f, -2f);
        moveDirection[2] = new Vector2(0f, -1f);
        moveDirection[3] = new Vector2(-3f, -2f);
        moveDirection[4] = new Vector2(-3f, 2f);

        for (int i = 0; i < bullets.Length; i++)
        {
            moveDirection[i].Normalize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            //if ((bullets[i] is UnityEngine.Object) && (!object.ReferenceEquals(bullets[i], null)) )
            if ((bullets[i] is UnityEngine.Object) && (bullets[i] !=null) )
                bullets[i].position += new Vector3(speed * moveDirection[i].x * Time.deltaTime, speed * moveDirection[i].y * Time.deltaTime, 0f);
        }
    }
}
