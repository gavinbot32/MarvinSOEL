using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Firebar_Script : MonoBehaviour
{
    [Header("Component / GameObjects")]
    public BoxCollider2D col;
    public Rigidbody2D rig;
    private GameObject fireParent;
    public GameObject fireBallPrefab;
    private GameObject player;
    private PlayerController player_script;
    [Header("Bar Size Variables")]
    [Tooltip("player height = 5 fireballs")]
    public int fireCount;
    public float initballY = 0.7f;
    public float fireballHeight;

    [Header("Misc.")]
    [Tooltip("Default: 1000 / Firecount")]
    public float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if(rotSpeed == 0)
        {
            rotSpeed = 1000 / fireCount;
        }
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        player_script = FindObjectOfType<PlayerController>();
        player = player_script.gameObject;
        float curBallY = 0;
        for (int i = 0; i < fireCount; i++)
        {
            if(i == 0)
            {
                GameObject ball = Instantiate(fireBallPrefab, gameObject.transform);
                ball.transform.position = new Vector3(transform.position.x, transform.position.y + initballY, transform.position.z);
                ball.transform.SetParent(transform, false);
                curBallY = ball.transform.position.y;
                col.size = new Vector2(0.3f, fireballHeight);
                col.offset = new Vector2(0, initballY);
            }
            else
            {
                GameObject ball = Instantiate(fireBallPrefab, gameObject.transform);
                ball.transform.position = new Vector3(transform.position.x, curBallY + fireballHeight, transform.position.z);
                ball.transform.SetParent(transform, false);
                curBallY = ball.transform.position.y;
                col.size = new Vector2(0.3f, col.size.y + fireballHeight);
                col.offset = new Vector2(0, col.offset.y + fireballHeight /2);
            }
        }
        
    }
    private void FixedUpdate()
    {
        rig.angularVelocity = rotSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player_script.die();
        }
    }
}
