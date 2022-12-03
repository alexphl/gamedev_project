using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemies;
    public Transform spawnPoints;
    private Transform[] spawns;

    public float spawnTimer;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTimer;
        spawns = new Transform[spawnPoints.childCount];

        int i = 0;
        foreach (Transform t in spawnPoints)
        {
            spawns[i] = t;
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
    }

    private void SpawnEnemies()
    {
        int[] intArray = new int[enemies.Length * 2];
        
        for (int i = 0; i < intArray.Length; i++)
        {
            intArray[i] = Mathf.FloorToInt(Random.Range(0, 15.9f));
        }

        int j = 0;
        foreach (Transform go in spawns)
        {
            foreach (int i in intArray)
            {
                if (i == j)
                {
                    int indexer = j % enemies.Length;
                    
                    Instantiate(enemies[indexer], spawns[i].position, Quaternion.identity);
                }
            }

            j++;
        }
    }
}
