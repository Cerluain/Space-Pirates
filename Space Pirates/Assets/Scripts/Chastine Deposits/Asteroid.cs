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
            TakeDamage(collision.collider.GetComponent<BulletScript>().damage);
        }
    }

    void TakeDamage(int damage)
    {
        print(damage);
            print("Touch");
            health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
