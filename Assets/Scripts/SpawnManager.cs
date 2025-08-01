using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float TimeBetweenSpawns = Random.Range(5,10);
    private int enemyAmount = Random.Range(1, 2);
    private Vector3 spawnPosition;

    public GameObject EnemyPrefab;
    void Start()
    {
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        if (EnemyPrefab != null)
        {
            for(int i; enemyAmount < i; i++)
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(TimeBetweenSpawns);
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
