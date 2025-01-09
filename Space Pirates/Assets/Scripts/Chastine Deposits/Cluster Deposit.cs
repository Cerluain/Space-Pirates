using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterDeposit : MonoBehaviour
{
    public int max_chastine_amount;
    public int min_chastine_amount;
    Rigidbody2D rb;
    public float CLUSTER_LIFETIME_CONSTANT;
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
        
        if (collision.collider.CompareTag("Player"))
        {
            PlayerScript p_script = collision.collider.GetComponent<PlayerScript>();
            p_script.GainHealthAndWeight(Random.Range(min_chastine_amount, max_chastine_amount));
            DestroyCluster();
        }
    }
    void SetLifespanOfBullet()
    {
        Invoke("DestroyCluster", CLUSTER_LIFETIME_CONSTANT);
    }
    void DestroyCluster()
    {
        Destroy(gameObject);
    }
}
