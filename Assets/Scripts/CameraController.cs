using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float minHeight, maxHieght;
    public float borderRight;
    public float posArenaX, posArenaY, scaleArena;
    public float playerArenaX, playerArenaY;
    private bool IsArena = false;

    public Animator animator;

    public Transform[] BackgroundNotMoving;
    public Transform[] Background4;
    public Transform[] Background3;
    public Transform[] Background2;
    public Transform[] Background1;

    private Vector2 lastPos;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((target.position.x > playerArenaX) && (target.position.y < playerArenaY))
        {
            //its Boss arena

            IsArena = true;
            animator.SetTrigger("arena");
        }
        if (IsArena)
        {
            transform.position = new Vector3(posArenaX, posArenaY, transform.position.z);
            //Camera.main.orthographicSize = scaleArena;
            // UnityEngine.Camera.main.orthographicSize = scaleArena;
            //UnityEngine.Camera.main.orthographicSize = UnityEngine.Camera.main.orthographicSize + 1 * Time.deltaTime;
            //if (UnityEngine.Camera.main.orthographicSize > 7)
            //{
                UnityEngine.Camera.main.orthographicSize = 7; // Max size
           // }
        }
        else if (IsArena==false)
        {
            UnityEngine.Camera.main.orthographicSize = 5;
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
       

            float clampedY = Mathf.Clamp(target.position.y, minHeight, maxHieght);
            if (transform.position.x > borderRight)
                transform.position = new Vector3(borderRight, clampedY, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
            //--------------paralax
        }
            float amountToMoveX = transform.position.x - lastPos.x;
            float amountToMoveY = transform.position.y - lastPos.y;

        
            for (int i = 0; i < BackgroundNotMoving.Length; i++)
            {
                BackgroundNotMoving[i].position += new Vector3(amountToMoveX, amountToMoveY, 0f);
            }
            for (int i = 0; i < Background4.Length; i++)
            {
                Background4[i].position += new Vector3(amountToMoveX, amountToMoveY*0.9f, 0f) * .995f;
            }
           for (int i = 0; i < Background3.Length; i++)
            {
                Background3[i].position += new Vector3(amountToMoveX, amountToMoveY*0.8f, 0f) * .99f;
            }
           for (int i = 0; i < Background2.Length; i++)
            {
                Background2[i].position += new Vector3(amountToMoveX, amountToMoveY*0.7f, 0f) * .98f;
            }
           for (int i = 0; i < Background1.Length; i++)
            {
                Background1[i].position += new Vector3(amountToMoveX, amountToMoveY*0.6f, 0f) * .97f;
            }

            lastPos = transform.position;

        
    }
}
