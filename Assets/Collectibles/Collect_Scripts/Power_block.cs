using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_block : MonoBehaviour
{

    public GameObject prefab;
    public GameObject powerup;
    public bool isActive;
    public SpriteRenderer sr;
    public BoxCollider2D col;
    public Vector2 pre_trigSize;
    public Vector2 pre_trigOff;
    public Vector2 post_size;
    public bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
    
        col = GetComponent<BoxCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        isMoving = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            sr.enabled = true;
            col.isTrigger = false;
            col.size = post_size;
            col.offset = new Vector2(0, 0);
        }
        else
        {
            sr.enabled = false;
            col.isTrigger = true;
            col.size = pre_trigSize;
            col.offset = pre_trigOff;
        }
        if (isMoving)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
            powerup.transform.position = Vector3.MoveTowards(powerup.transform.position,targetPos, 5f * Time.deltaTime);
            if(Math.Abs(powerup.transform.position.y - targetPos.y) <= 0.01f)
            {
                powerup.transform.position = targetPos;
                isMoving = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.y + 0.5f <= transform.position.y - 0.5f)
            {

                if (!isActive)
                {
                    isActive = true;
                }
                powerup = Instantiate(prefab, transform.position, Quaternion.identity);
                isMoving = true;
            }
        }
    }
}
