using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyPos;

    private bool canSpown;

    void Start()
    {
        canSpown = true;
    }

    void Update()
    {
        if (canSpown)
        {
            StartCoroutine("SpawnEnemy");
        }
    }
    IEnumerator SpawnEnemy()
    {
       
        Instantiate(enemy, enemyPos.transform.position, Quaternion.identity);
        canSpown = false;
        yield return new WaitForSeconds(2f);
        canSpown = true;
        
    }
}
