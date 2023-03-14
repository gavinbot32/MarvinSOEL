using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int value;
    public float speed;
    public bool isCollect;
    private Animator anim;
    private PlayerController player;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        isCollect = false;
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
        if (isCollect)
        {
            anim.SetBool("isCollect", isCollect);
            playerMove();   
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collect();   
        }
    }

    private void playerMove()
    {
        transform.position = Vector3.MoveTowards(transform.position,playerPos, speed * Time.deltaTime);
        
    }
    private void collect()
    {
        isCollect=true;
        PlayerController player = FindObjectOfType<PlayerController>();
        player.points += value;
        Destroy(gameObject, 0.7f);

    }


}
