using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Player game properties
    public int health;
    public int load_capacity;
    public int materials;

    // Player attributes
    Vector2 mouse_position;
    GameObject current_weapon;
    Rigidbody2D player_body;

    // Control variables
    private Vector2 target_direction;

    void ShootWeapon(Vector2 mouse_direction)
    {
        bool canFire = UpdateMaterialsForShooting(current_weapon.GetComponent<NormalWeaponScript>());
        if (canFire)
            current_weapon.GetComponent<NormalWeaponScript>().FireWeapon(mouse_direction, player_body); 
    }

    bool UpdateMaterialsForShooting(NormalWeaponScript weapon)
    {
        if (materials - weapon.material_cost < 0){
            materials = 0;
            return false;
        }

        materials -= weapon.material_cost;
        UpdateHealthAndWeight(materials); //Only update if it was valid to even shoot
        return true;
    }
    private void ShipHasTakenAHit(int dmg)
    {
        ReduceHealthAndWeight(dmg);
    }

    private void UpdateHealthAndWeight(int new_health)
    {
        health = new_health;
        player_body.mass = new_health;
        materials = new_health;
        UpdateHealthBar();
    }
    private void ReduceHealthAndWeight(int reduction_amount)
    {
        health -= reduction_amount;

        if (health <= 0) Destroy(gameObject);
        UpdateHealthAndWeight(health);
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        GameObject.Find("Health Meter").GetComponent<MaterialDisplay>().UpdateToShowKAmountOfMaterials(materials);
    }
    public void GainHealthAndWeight(int gain_amount)
    {
        health += gain_amount;

        UpdateHealthAndWeight(health);
    }

    void Start()
    {
        player_body = GetComponent<Rigidbody2D>();
        current_weapon = GameObject.Find("Weapon");
        materials = 50;
        UpdateHealthAndWeight(materials);
    }

    // Update is called once per frame
    void Update()
    {
        mouse_position = Input.mousePosition;
        mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);
        target_direction = mouse_position - player_body.position;

        if (Input.GetMouseButtonDown(0))
            ShootWeapon(target_direction);

    }  
    private void FixedUpdate()
    {
        float player_angle_offset = Mathf.Atan2(target_direction.y, target_direction.x)* Mathf.Rad2Deg;
        player_body.rotation = player_angle_offset;

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
}
