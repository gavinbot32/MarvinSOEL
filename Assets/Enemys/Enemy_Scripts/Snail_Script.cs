using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Snail_Script : MonoBehaviour
{
    public bool isDead;
    public int moveDir;
    public float moveSpeed;
    public Rigidbody2D rig;
    public bool isActive;
    public GameObject player;
    public PlayerController player_script;
    public Animator anim;

    public LayerMask layer;
    public bool ableMove;
    // Start is called before the first frame update
    protected void Start()
    {
        isDead = false;
        isActive = false;
        rig = GetComponent<Rigidbody2D>();
        moveDir = -1;
        player = GameObject.FindGameObjectWithTag("Player");
        player_script = player.GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();
        ableMove = true;
    }


    public void die()
    {
        ableMove = false;
        isDead = true;
        Destroy(gameObject, 1f);
    }
    private void move()
    {
        if (ableMove)
        {
            transform.localScale = new Vector3(moveDir * -1, transform.localScale.y, transform.localScale.z);
            rig.velocity = new Vector2(moveSpeed * moveDir, rig.velocity.y);
        }
    }


    // Update is called once per frame
    protected void Update()
    {
        

        if (isActive)
        {
            anim.SetBool("isDead", isDead);
            move();
            check_for_wall();
        }
        
    }

    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(player_script.isJumping || player_script.rig.velocity.y < -0.1f)
            {
                die();
                

            }
            else
            {
                player_script.die();
            }
        }
        
    }

    private void check_for_wall()
    {
        Debug.DrawRay(transform.position, Vector2.right * moveDir, Color.blue, .75f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * moveDir,1f,layer);
        if (hit) {
            moveDir *= -1;
        }
    }


}
