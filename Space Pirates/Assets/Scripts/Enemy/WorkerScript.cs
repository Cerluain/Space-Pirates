using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class WorkerScript : MonoBehaviour
{
    //Worker attributes
    public int INITIAL_HEALTH = 50;
    public int health;
    Rigidbody2D rb;
    public float shooting_cooldown = 5f;
    private float cooldown_since_last_shot = 0f;

    private bool delivers_goods;
    private Vector2 current_destination_position;
    private Vector2 vector_to_target;

    //Assign task to worker: Deliver chastine or Acquire goods
    private static int goods_delivery_workers_count = 0;
    private static int chastine_delivery_workers_count = 0;

    //Other related variables
    private GameObject assigned_delivery_item;

    private Transform delivery_position;
    private Collider2D delivery_collider;
    private Transform pickup_position;
    private Collider2D pickup_collider;

    Rigidbody2D player_rb;
    MovementWeapon weapon_script;

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();

        UpdateHealthAndWeight(INITIAL_HEALTH);

        weapon_script = GetComponentInChildren<MovementWeapon>();

        AssignToTaskForWorker();
    }

    void AssignToTaskForWorker()
    {
        delivers_goods = (goods_delivery_workers_count <= chastine_delivery_workers_count)? true: false;
        
        if (delivers_goods) BecomeGoodsDeliveryWorker();
        else BecomeChastineDeliveryWorker();
    }
    private void BecomeGoodsDeliveryWorker()
    {
        goods_delivery_workers_count++;

        GameObject outpost_delivery = GameObject.Find("GoodsDeliveryArea");
        GameObject cargo_pickup = null; //GameObject.Find("");

        delivery_position = outpost_delivery.GetComponent<Transform>(); //Outpost delivery area
        delivery_collider = outpost_delivery.GetComponent<Collider2D>();
        pickup_position = cargo_pickup.GetComponent<Transform>(); ; //Cargo collecting area 
        pickup_collider = cargo_pickup.GetComponent<Collider2D>();

    }
    private void BecomeChastineDeliveryWorker()
    {
        chastine_delivery_workers_count++;

        GameObject chastine_pickup = GameObject.Find("ChastinePickupArea");
        GameObject cargo_delivery = null; //GameObject.Find("");
        
        delivery_position = cargo_delivery.GetComponent<Transform>(); //Outpost collect area
        delivery_collider = cargo_delivery.GetComponent<Collider2D>();
        pickup_position = chastine_pickup.GetComponent<Transform>(); //Cargo collecting area 
        pickup_collider = chastine_pickup.GetComponent<Collider2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        vector_to_target = current_destination_position - rb.position;
        FaceInDirection(-vector_to_target);

        if (UpdateCooldownAndCheckIfEnemyShouldFire())
            ShootBackwards();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D object_touched = collision.collider;
        if (object_touched.CompareTag("Bullet"))
        {
            int damage_taken = object_touched.GetComponent<BulletScript>().damage;

            if (collision.collider.GetComponent<BulletScript>().isCollisionValidAndUpdateList(gameObject))
                ShipHasTakenAHit(damage_taken);
        }
    }

    private void ShootBackwards()
    {
        weapon_script.FireWeapon(-vector_to_target, rb);
        cooldown_since_last_shot = 0f;
    }
    private bool UpdateCooldownAndCheckIfEnemyShouldFire()
    {
        cooldown_since_last_shot += Time.deltaTime;
        return cooldown_since_last_shot >= shooting_cooldown; //Time to shoot
    }

    private void FaceInDirection(Vector2 target_direction)
    {
        float angle_offset = Mathf.Atan2(target_direction.y, target_direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle_offset;
    }

    private void ShipHasTakenAHit(int dmg)
    {     ReduceHealthAndWeight(dmg);    }

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
