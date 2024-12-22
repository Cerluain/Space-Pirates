using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int load_capacity;
    public int materials;
    public Vector2 mouse_position;
    GameObject current_weapon;
    Rigidbody2D player_body;
    //Weapon selected_weapon

    void ShootWeapon(Vector2 mouse_direction)
    {
        player_body.mass = materials;
        current_weapon.GetComponent<NormalWeaponScript>().FireWeapon(mouse_direction, player_body);
        /* switch (selected_weapon)
        {
            case BoomerangWeapon: 
                break;
            case ExplosiveWeapon: 
                break;
            case default:
                break;
        }*/
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
        if (Input.GetMouseButtonDown(0))
        {
            mouse_position = Input.mousePosition;
            mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);
            Vector2 target_direction = mouse_position - player_body.position;
            print(mouse_position);
            print(target_direction);
            ShootWeapon(target_direction);
        }
        
    } 
}
