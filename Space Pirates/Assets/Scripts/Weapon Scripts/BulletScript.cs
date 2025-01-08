using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class BulletScript : MonoBehaviour
{
    public float BULLET_LIFETIME_CONSTANT = 10f; //The higher the longer the bullet sticks 

    public int damage; //Usually based on mass, but not necessarily
    public int enemy_penetrations_remaining;

    //Could add a trigger but not all of them need trigger, code below is temporary:
    public bool needs_trigger;
    public bool trigger_activated;

    //Bullet general global variables
    Rigidbody2D rb;
    ArrayList objects_hit_already;

    public void InitializeBullet(int p_damage, int p_durability)
    {
        rb = GetComponent<Rigidbody2D>();

        damage = p_damage;
        enemy_penetrations_remaining = p_durability;
        objects_hit_already = new ArrayList();

        Invoke("SetLifespanOfBullet", 0.5f);
    }

    public bool isCollisionValidAndUpdateList(GameObject object_hit)
    {
        if (objects_hit_already.Contains(object_hit))
            return false;

        objects_hit_already.Add(object_hit);
        removeDurability();
        return true;
    }
    private void removeDurability()
    {
        enemy_penetrations_remaining--;
        if (enemy_penetrations_remaining <= 0)
            Destroy(gameObject);
    }
    void SetLifespanOfBullet()
    {
        float life_expectancy = 8f + BULLET_LIFETIME_CONSTANT / rb.velocity.magnitude;
        life_expectancy = (life_expectancy < 700f && life_expectancy > 0f) ? life_expectancy : 120f;
        Invoke("DestroyBullet", life_expectancy);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}