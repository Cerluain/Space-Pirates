using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerWeaponScript : MonoBehaviour
{
    //Constants for tweaking the 
    public float WEIGHT_PER_MATERIAL = 10f;

    public int damage = 5;
    public int shot_durability = 1;
    public int material_cost = 10;
    public int shot_count = 30;
    public int speed = 8;
    public GameObject bullet_prefab;

    // Start is called before the first frame update

    void Start()
    { 
        //GetComponent<Transform>().SetParent(player_transform);
    }

    // Update is called once per frame
    void Update() { }

    public GameObject generate_bullet(Rigidbody2D attacker_rb)
    {
        // Instantiate the prefab at the current position and rotation
        GameObject bullet = Instantiate(bullet_prefab, transform.position, transform.rotation);
        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();
        bullet_rb.mass = material_cost;
        bullet_rb.velocity = attacker_rb.velocity / 2;
        return bullet;
    }

    public void FireWeapon(Vector2 shooting_direction, Rigidbody2D weapon_user_rigidbody)
    {
        //Creates a bullet prefab and passes weapon properties into the bullet shot
        GameObject new_bullet = generate_bullet(weapon_user_rigidbody);
        float shooting_force = WEIGHT_PER_MATERIAL * material_cost * speed;
        Vector2 force_of_interaction = shooting_direction.normalized * shooting_force;

        new_bullet.GetComponent<Rigidbody2D>().AddForce(force_of_interaction);
        weapon_user_rigidbody.AddForce(-force_of_interaction);

        new_bullet.GetComponent<BulletScript>().InitializeBullet(damage, shot_durability);
    }
}
