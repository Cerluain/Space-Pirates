using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerScript : MonoBehaviour
{
    public int health;

    Rigidbody2D rb;
    Collider2D this_collider;
    Vector2 vector_to_player;
    public float shooting_cooldown = 5f;
    private float cooldown_since_last_shot = 0f;
    public float required_distance_to_shoot = 5f;

    //Other variables
    private GameObject player;
    Rigidbody2D player_rb;
    AttackerWeaponScript weapon_script;


    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        this_collider = GetComponent<Collider2D>();

        UpdateHealthAndWeight(50);

        //Relationship Variables
        player = GameObject.FindGameObjectWithTag("Player");
        player_rb = player.GetComponent<Rigidbody2D>();
        weapon_script = GetComponentInChildren<AttackerWeaponScript>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vector_to_player = player_rb.position - rb.position;
        

        if (UpdateCooldownAndCheckIfEnemyShouldFire())
            FireWeapon();
        else
            FaceInDirection(IsPlayerInOurShootingRange()? vector_to_player: -vector_to_player);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D object_touched = collision.collider;
        if (object_touched.CompareTag("Bullet")) {
            int damage_taken = object_touched.GetComponent<BulletScript>().damage;

            if(collision.collider.GetComponent<BulletScript>().isCollisionValidAndUpdateList(gameObject))
                ShipHasTakenAHit(damage_taken);
        }
    }
    private void FireWeapon()
    {
        //Should we get closer or shoot at player?
        if (IsPlayerInOurShootingRange())
        { 
            //Shoot at player
            weapon_script.FireWeapon(vector_to_player, rb);
            cooldown_since_last_shot = 0f;
        }
        else
        {
            //Shoot three times backwards
            ExtraShotBackwards();
            cooldown_since_last_shot = 0f;
            Invoke("ExtraShotBackwards", .25f);
            Invoke("ExtraShotBackwards", .5f);
        }
    }
    private void ExtraShotBackwards()
    {
        weapon_script.FireWeapon(-vector_to_player, rb);
    }
    private bool UpdateCooldownAndCheckIfEnemyShouldFire()
    { 
        cooldown_since_last_shot += Time.deltaTime;
        return cooldown_since_last_shot >= shooting_cooldown; //Time to shoot
        
    }
    private bool IsPlayerInOurShootingRange()
    { return vector_to_player.magnitude <= required_distance_to_shoot; }

    private void FaceInDirection(Vector2 target_direction)
    {
        float angle_offset = Mathf.Atan2(target_direction.y, target_direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle_offset;
    }

    private void ShipHasTakenAHit(int dmg)
    {
        ReduceHealthAndWeight(dmg);
    }
    
    private void UpdateHealthAndWeight(int new_health)
    {
        health = new_health;
        rb.mass = new_health;
    }
    private void ReduceHealthAndWeight(int reduction_amount)
    {
        health -= reduction_amount;

        if (health <= 0) Destroy(gameObject);
        rb.mass = health;
    }
}
