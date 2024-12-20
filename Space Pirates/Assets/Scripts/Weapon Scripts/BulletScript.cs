using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public Vector2 dir_vector;
    public int damage; //Usually based on mass, but not necessarily
    public int enemy_penetrations_remaining;
    
    //Could add a trigger but not all of them need trigger, code below is temporary:
    public bool needs_trigger;
    public bool trigger_activated;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
