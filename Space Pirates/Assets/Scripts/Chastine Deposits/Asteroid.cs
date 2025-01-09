using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int health;
    public int number_of_deposits;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 50;
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            BulletScript b_script = collision.collider.GetComponent<BulletScript>();
            if (b_script.isCollisionValidAndUpdateList(gameObject))
                TakeDamage(b_script.damage);
        }
    }
    void TakeDamage(int damage)
    {
            health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
