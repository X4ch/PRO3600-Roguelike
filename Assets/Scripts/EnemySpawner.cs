using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawner;
    public GameObject enemy;
    public Transform spawnerTransform;

    // Permet de faire apparaitre un ennemie choisit
    public void spawnEnemy()
    {
        Instantiate(enemy, spawnerTransform.position, Quaternion.identity);
        Destroy(spawner);
    }
}
