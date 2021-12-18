using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float minHeight, maxHieght;

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

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        float clampedY = Mathf.Clamp(target.position.y, minHeight, maxHieght);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        //--------------paralax
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
