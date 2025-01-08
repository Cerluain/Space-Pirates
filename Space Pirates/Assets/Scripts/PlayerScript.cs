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
        if(canFire)
            current_weapon.GetComponent<NormalWeaponScript>().FireWeapon(mouse_direction, player_body);
        /* switch (selected_weapon){
            case BoomerangWeapon: 
                break;
            case ExplosiveWeapon: 
                break;
            case default:
                break;
        }*/
    }

    bool UpdateMaterialsForShooting(NormalWeaponScript weapon)
    {
        materials -= weapon.material_cost;
        player_body.mass = materials;
        if(materials < 0){
            materials = 0;
            return false;
        }
        return true;
    }

    void Start()
    {
        player_body = GetComponent<Rigidbody2D>();
        current_weapon = GameObject.Find("Weapon");
        materials = 100;
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
}
