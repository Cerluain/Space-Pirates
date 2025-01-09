using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsScript : MonoBehaviour
{
    public float pickup_immunity_time = 2f;

    private float time_when_dropped;
    private bool is_being_held;
    private GameObject last_owner;

    // Start is called before the first frame update
    private void Start()
    {
        is_being_held = false;
        time_when_dropped = Time.time; //Just to avoid leaving it undefined
    }
    public void TryToPickUpBox(GameObject parent)
    {
        if (!is_being_held && last_owner == parent && Time.time - time_when_dropped < pickup_immunity_time) //These make it invalid to pick it up
        {
            return;
        }
        else 
            PickUpBox(parent.transform); //If Valid
    }
    private void PickUpBox(Transform parent)
    {
        transform.SetParent(parent);
        is_being_held = true;
    }
    void DropThisBox()
    {
        transform.SetParent(null);
        is_being_held = false;
        time_when_dropped = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
