using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving_platform : MonoBehaviour
{

    private Target targetObj;
    public Vector3 startPos;
    public Vector3 targetPos;
    public float moveSpeed;

    private bool movingToTarget;

    private void Awake()
    {
        startPos = transform.position;
        targetObj = GetComponentInChildren<Target>();
        targetPos = targetObj.transform.position;
        movingToTarget = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();

    }

    private void move()
    {
        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetPos, moveSpeed * Time.deltaTime);
            if(transform.position == targetPos)
            {
                movingToTarget = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            if (transform.position == startPos)
            {
                movingToTarget = true;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {

        }
        else
        {
            collision.transform.SetParent(transform);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
        }
        else
        {
            collision.transform.SetParent(null);

        }
    }
}
