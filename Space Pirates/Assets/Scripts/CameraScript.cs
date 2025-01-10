using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    float MAX_CHANGE_SPEED = .15f;
    Transform player_transform;
    Rigidbody2D player_rb;
    Camera cam;
    float original_cam_size;
    void Start()
    {
        cam = GetComponent<Camera>();
        original_cam_size = cam.orthographicSize;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player_rb = player.GetComponent<Rigidbody2D>();
        player_transform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player_transform.position.x, player_transform.position.y, transform.position.z);

        float total_change_in_size = Mathf.Sqrt(player_rb.velocity.magnitude)+original_cam_size - cam.orthographicSize;
        if (total_change_in_size == 0) return;
        float increment_amount = MAX_CHANGE_SPEED * Time.deltaTime;
        float total_change_sign = (Mathf.Abs(total_change_in_size) / total_change_in_size);
        cam.orthographicSize = cam.orthographicSize + Mathf.Min(Mathf.Abs(total_change_in_size), total_change_sign * increment_amount);

        
    }
}