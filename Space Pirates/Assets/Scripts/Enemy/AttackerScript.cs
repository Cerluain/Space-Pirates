using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerScript : MonoBehaviour
{
    public int health;

    Rigidbody2D rb;
    Collider2D this_collider;
    //Weapon weapon = ;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this_collider = GetComponent<Collider2D>();
        updateHealthAndWeight(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D object_touched = collision.collider;
        if (object_touched.CompareTag("Bullet")) {
            int damage_taken = object_touched.GetComponent<BulletScript>().damage;

            if(collision.collider.GetComponent<BulletScript>().isCollisionValidAndUpdateList(gameObject))
                shipHasTakenAHit(damage_taken);
        }

    }

    private void shipHasTakenAHit(int dmg)
    {
        reduceHealthAndWeight(dmg);
    }
    
    private void updateHealthAndWeight(int new_health)
    {
        health = new_health;
        rb.mass = new_health;
    }
    private void reduceHealthAndWeight(int reduction_amount)
    {
        health -= reduction_amount;

        if (health <= 0) Destroy(gameObject);
        rb.mass = health;
    }
}
