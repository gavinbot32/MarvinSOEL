using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dupe_Slime : Snail_Script
{
    public GameObject slimePrefab;
    public float splitForce;
    public float playerBoundForce;
    public int index;
    public float spawnTime;
    public float spawnInterval;
    private bool countTime;
    public PhysicsMaterial2D physMat;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        spawnTime = 0;
        countTime = false;
        col = GetComponent<Collider2D>();
        col.sharedMaterial = null;
        
    }

    void Update()
    {
        base.Update();
        if (countTime)
        {
            if (spawnTime > 0)
            {
                spawnTime -= spawnTime * Time.deltaTime;
                if(spawnTime <= 1)
                {
                    spawnTime = 0;
                }
            }
            else
            {
                ableMove = true;
                countTime = false;
            }
        }

    }

    private void FixedUpdate()
    {
        Vector2 playerPos = player.transform.position;
        if(playerPos.x - transform.position.x <= 1 && playerPos.y - transform.position.y <= 1){
            col.sharedMaterial = physMat;
        }
        else
        {
            col.sharedMaterial = null;

        }
        
    }

    public void split()
    {

        if(index < 2)
        {
            if (spawnTime <= 0)
            {
                ableMove = false;
                spawnTime = spawnInterval; 
                countTime = true;
                GameObject slime = Instantiate(slimePrefab, transform.position, Quaternion.identity);
                index++;
                Dupe_Slime dupeSlime = slime.GetComponent<Dupe_Slime>();
                dupeSlime.spawnTime = spawnInterval;
                dupeSlime.countTime = true; 
                dupeSlime.index++;
                rig.AddForce(Vector2.left * splitForce, ForceMode2D.Impulse);
                dupeSlime.rig.AddForce(Vector2.right * splitForce, ForceMode2D.Impulse);
            }
        }
        

    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            
            split();


        }
    }

}
