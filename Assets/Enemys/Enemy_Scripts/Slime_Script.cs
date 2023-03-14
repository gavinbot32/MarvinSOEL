using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Slime_Script : Snail_Script
{
    public bool movingToTarget;
    public Vector3 targetPos;
    public Vector3 startPos;
    public Slime_Target target;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        startPos = transform.position;
        target = GetComponentInChildren<Slime_Target>();
        targetPos = new Vector3(target.transform.position.x, startPos.y, startPos.z);
        movingToTarget = true;

    }


    public void move()
    {
        transform.localScale = new Vector3(moveDir * -1, transform.localScale.y, transform.localScale.z);

        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            if (transform.position.x == targetPos.x)
            {
                movingToTarget = false;
                moveDir *= -1;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            if (transform.position.x == startPos.x)
            {
                movingToTarget = true;
                moveDir *= -1;

            }

        }
    }
    // Update is called once per frame
    protected void Update()
    {
        if (isActive)
        {
            anim.SetBool("isDead", isDead);
            move();
        }
        
    }
    private void OnBecameInvisible()
    {
        isActive = false;
    }
}
