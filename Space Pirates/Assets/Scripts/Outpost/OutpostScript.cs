using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class OutpostScript : MonoBehaviour
{
    public int max_ship_count;
    public int min_ship_count;
    public List<GameObject> human_ships = new List<GameObject>();
    public List<GameObject> robot_ships = new List<GameObject>();
    public List<GameObject> enemy_flags = new List<GameObject>();
    private int number_of_ships;
    private int flag_index;
    private int human_ships_index;
    private int robot_ships_index;
    public int position_1;
    public int position_2;
    public Vector3 attacker_spawn_offset;
    public Vector3 flag_position;

    private List<GameObject> selected_ships;
    public float interval_between_spawns;
    

    // Start is called before the first frame update
    void Start()
    {
        SelectFlag();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectFlag()
    {
        flag_index = UnityEngine.Random.Range(0, enemy_flags.Count);
        print(flag_index);
        Instantiate(enemy_flags[flag_index], transform.position + flag_position, Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = 5;
        SpawnShips();
    }
    void SpawnWorkerShip()
    {
        float x_position = UnityEngine.Random.Range(0,2)==0? position_1:position_2;
        Vector3 worker_spawn_offset = new Vector3(x_position, 1, 0);
        Instantiate(selected_ships[1], transform.position + worker_spawn_offset, Quaternion.identity);
    }
    void SpawnAttackerShip()
    {
        attacker_spawn_offset = new Vector3(0, 7, 0);
        Instantiate(selected_ships[0], transform.position + attacker_spawn_offset, Quaternion.identity);
    }
    void SpawnShips()
    {
        if (flag_index == 0) selected_ships = human_ships;
        else selected_ships = robot_ships;

        int random_number_of_ships = UnityEngine.Random.Range(min_ship_count, max_ship_count);
        int number_of_attacker_ships = (int)Mathf.Round((float)random_number_of_ships * 2 / 3);
        int number_of_worker_ships = random_number_of_ships - number_of_attacker_ships;
        


        for (float time_until_spawn = 0; time_until_spawn < interval_between_spawns*number_of_attacker_ships; time_until_spawn += interval_between_spawns)
        {
            Invoke("SpawnAttackerShip", time_until_spawn);
            
        }
        for (float time_until_spawn = 0; time_until_spawn < interval_between_spawns * number_of_worker_ships; time_until_spawn += interval_between_spawns)
        {
            Invoke("SpawnWorkerShip", time_until_spawn);

        }

    }
    
}
