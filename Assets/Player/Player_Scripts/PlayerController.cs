using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Debug")]
    public bool debug;
    public bool isDead;

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator animator;
    public AudioSource audio;
    public Collider2D collider;
    public int points;

    [Header("Movement Vars")]
    public float x_input;
    public float y_input;
    public float moveSpeed;
    public float climbSpeed;
    public bool isClimbing;
    public int facingDir;
    public bool isWalking;
    [Header("Jumping Vars")]
    public float jumpForce;
    public int maxJumps;
    public int jumpCount;
    public bool isJumping;
   
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        
    }



    // Start is called before the first frame update
    void Start()
    {
        x_input = 0f;
        y_input = 0f;
        isJumping = false;
        isClimbing = false;
        isDead = false;
        facingDir = 1;

        maxJumps = 2;
        
    }

    private void FixedUpdate()
    {
        x_input = Input.GetAxis("Horizontal");
        move(x_input);

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDead", isDead);


        if (rig.velocity.y <= 0)
        {
            if (isGrounded()){
                reset_jump_count();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        if (debug)
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y-0.5f, transform.position.z), Vector2.down, Color.red,.01f);
            Debug.DrawRay(new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, transform.position.z), Vector2.down, Color.red,.01f);
            Debug.DrawRay(new Vector3(transform.position.x+0.5f, transform.position.y - 0.5f,transform.position.z),Vector2.down, Color.red,.01f);
            Debug.DrawRay(new Vector3(transform.position.x-0.5f, transform.position.y +0.5f,transform.position.z),new Vector2(-0.5f,0.5f), Color.red,.01f);
            Debug.DrawRay(new Vector3(transform.position.x+0.5f, transform.position.y + 0.5f,transform.position.z),new Vector2(0.5f,0.5f), Color.red,.01f);
        }
    }
    public void reset_jump_count()
    {
        jumpCount = maxJumps;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void jump()
    {
       
        if(jumpCount > 0)
        {
            jumpCount--;
            isJumping = true;
            isWalking = false;
            if (rig.velocity.y<= -0.01f)
            {
                rig.velocity = new Vector2(rig.velocity.x, 0);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    private void move(float x_input)
    {
        if(x_input != 0 && !isJumping)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        if (x_input > 0)
        {
            facingDir = 1;
        }
        else if (x_input < 0)
        {
            facingDir = -1;
        }
        transform.localScale = new Vector3(facingDir, transform.localScale.y, transform.localScale.z);
        rig.velocity = new Vector2(x_input * moveSpeed, rig.velocity.y);

    }

    private void climb()
    {

    }

    private void crouch()
    {

    }

    public void die()
    {
        isDead = true;
        Destroy(gameObject,1f);
    }
    private bool isGrounded()
    {
        
        RaycastHit2D hitCenter = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y-0.5f,transform.position.z),Vector2.down, .01f);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(transform.position.x-0.5f, transform.position.y-0.5f,transform.position.z),Vector2.down, .01f);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(transform.position.x+0.5f, transform.position.y-0.5f,transform.position.z),Vector2.down, .01f);
        RaycastHit2D[] hitCasts = { hitCenter, hitLeft, hitRight };
        foreach(RaycastHit2D cast in hitCasts)
        {
            if (cast)
            {
                if (cast.collider.gameObject.CompareTag("Ground"))
                {

                    isJumping = false;
                    return true;

                    
                    
                }
            }

        }
         return false;
    }

    private void shoot()
    {

    }



}
