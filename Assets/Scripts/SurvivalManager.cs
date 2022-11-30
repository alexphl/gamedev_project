using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalManager : MonoBehaviour
{
    public GameObject spawnPoints;
    public float spawnTimer;
    public GameObject enemy1;
    public GameObject enemy4;
    public GameObject enemy3;
    public GameObject enemy2;

    int playerHealth;

    float timer;

    public GameObject player;

    GameObject[] spawns = new GameObject[16];
    GameObject[] enemies = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTimer;
        

        enemies[0] = enemy1;
        enemies[1] = enemy4;
        enemies[2] = enemy3;
        enemies[3] = enemy2;
        int i = 0;
        foreach (Transform t in spawnPoints.transform)
        {
            spawns[i] = t.gameObject;
            i++;
        }
        
        
    }

    private void Update()
    {
        timer -= Time.deltaTime * 1000f;
        if (timer <= 0f)
        {
            SpawnEnemies();
            timer = spawnTimer;
        }

        playerHealth = player.GetComponent<Player>().health;

        if (playerHealth <= 0)
        {
            Destroy(player);
        }
    }

    private void SpawnEnemies()
    {
        int[] intArray = new int[8];
        
        for (int i = 0; i < 8; i++)
        {
            intArray[i] = Mathf.FloorToInt(Random.Range(0, 15.9f));
        }
        int j = 0;
        foreach (GameObject go in spawns)
        {
            foreach (int i in intArray)
            {
                if (i == j)
                {

                    int indexer = j % 4;
                    
                    Instantiate(enemies[indexer], spawns[i].transform.position, Quaternion.identity);
                    
                }
            }
            j++;
        }

        
    }

}
