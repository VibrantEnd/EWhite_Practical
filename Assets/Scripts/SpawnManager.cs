using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float TimeBetweenSpawns;
    private int enemyAmount;

    public GameObject EnemyPrefab1;
    public GameObject EnemyPrefab2;
    public GameObject EnemyPrefab3;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        while(player.GetComponent<Player>().NewScore < 300)
        {
            TimeBetweenSpawns = Random.Range(10, 20);
            enemyAmount = Random.Range(1, 2);
            Debug.Log("Spawning has begun!");

                for (int i = 0; enemyAmount > i; i++)
                {
                    float randomizer = Random.Range(0, 3);
                    if (randomizer <= 1)
                    {
                        SpawnPrefab(EnemyPrefab1);
                    }
                    else if (randomizer <= 2)
                    {
                        SpawnPrefab(EnemyPrefab2);
                    }
                    else if (randomizer > 2)
                    {
                        SpawnPrefab(EnemyPrefab3);
                    }

            }
            yield return new WaitForSeconds(TimeBetweenSpawns);
        }
    }
    private void SpawnPrefab(GameObject EnemyPrefab)
    {
        Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
