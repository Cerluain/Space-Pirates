using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int health;
    public List<GameObject> prefabList = new List<GameObject>();
    public int max_cluster_count;
    public int min_cluster_count;
    public float max_position;
    public float min_position;

    // Start is called before the first frame update
    void Start()
    {

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
            print("Touch");
            health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            SpawnClusters();
        }
    }

    void SpawnClusters()
    {
        int prefab_index;
        int random_number_of_clusters = UnityEngine.Random.Range(min_cluster_count, max_cluster_count);
        Vector3 transform_position;
        float y_position;
        float x_position;

        for (int i = 0; i < random_number_of_clusters; i++)
        {
            y_position = UnityEngine.Random.Range(min_position, max_position);
            x_position = UnityEngine.Random.Range(min_position, max_position);
            transform_position = new Vector3(x_position, y_position, 0);
            prefab_index = UnityEngine.Random.Range(0, prefabList.Count - 1);
            Instantiate(prefabList[prefab_index], transform.position + transform_position, Quaternion.identity);
        }

    }
}
