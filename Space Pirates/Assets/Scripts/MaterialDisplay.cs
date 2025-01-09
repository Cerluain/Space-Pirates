using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDisplay : MonoBehaviour
{
    //Dimensions
    float top_offset;
    float bot_offset;
    float pellet_radius;
    float horizontal_pellet_offset;


    // Start is called before the first frame update
    Transform top_section_transform;
    Transform bottom_section_transform;
    ArrayList pellet_objects;

    //Prefabs of object
    public GameObject material_prefab;

    void Start()
    {
        top_section_transform = transform.Find("Player Health Top Half").GetComponent<Transform>();
        bottom_section_transform = transform.Find("Player Health Bottom Half").GetComponent<Transform>();

        top_offset = .185f;
        bot_offset = -.08f;
        pellet_radius = .11f;
        horizontal_pellet_offset = .2f;

        pellet_objects = new ArrayList();
    }
    private void ClearPastPellets()
    {
        foreach(GameObject pellet_obj in pellet_objects)
        {
            Destroy(pellet_obj);
        }
    }
    public void UpdateToShowKAmountOfMaterials(int material_num)
    {
        ClearPastPellets();

        int pellet_height = (int) Mathf.Ceil(material_num / 2);

        float total_height = (pellet_radius * 2 * pellet_height) + top_offset + bot_offset;

        float current_height = -total_height / 2;
        
        //Bottom
        bottom_section_transform.localPosition = new Vector2(0,current_height);

        //Update Heights
        current_height += pellet_radius + bot_offset;

        Vector3 pellet_diameter_offset = new Vector3(0, pellet_radius * 2, 0);
        
        Vector3 position_vec_left = new Vector3(-horizontal_pellet_offset, current_height, 0);
        Vector3 position_vec_right = new Vector3(horizontal_pellet_offset, current_height, 0);
        //Pellets on the middle
        for (int pellet_count = 0; pellet_count < pellet_height; pellet_count++)
        {
            position_vec_left += pellet_diameter_offset;
            position_vec_right += pellet_diameter_offset;
            GameObject left_pellet = Instantiate(material_prefab, transform.position + position_vec_left, Quaternion.identity);
            left_pellet.transform.SetParent(transform);
            pellet_objects.Add(left_pellet);

            GameObject right_pellet = Instantiate(material_prefab, transform.position    + position_vec_right, Quaternion.identity);
            right_pellet.transform.SetParent(transform);
            pellet_objects.Add(right_pellet);
        }
        current_height = position_vec_left.y + top_offset + pellet_radius;
        top_section_transform.localPosition = new Vector2(0, current_height);
    }
}
