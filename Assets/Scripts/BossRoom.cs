using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public bool currentRoom;
    public bool activeEnemy;
    public GameObject room;
    public List<GameObject> doorList;
    public GameObject[] enemyList;
    public List<GameObject> spawnerList;
    public GameObject nextFloorObject;

    [SerializeField] private Transform roomCords;

    public Transform GetPosition()
    {
        return roomCords;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentRoom = true;

            for (int i = 0; i < room.transform.childCount; i++)
            {
                if (room.transform.GetChild(i).tag == "Door")
                {
                    doorList.Add(room.transform.GetChild(i).gameObject);
                }

                if (room.transform.GetChild(i).tag == "Spawner")
                {
                    spawnerList.Add(room.transform.GetChild(i).gameObject);
                }
                if(room.transform.GetChild(i).tag == "NextFloor")
                {
                    nextFloorObject = room.transform.GetChild(i).gameObject;
                }
            }

            if (spawnerList.Count != 0)
            {
                foreach (GameObject spawner in spawnerList)
                {
                    spawner.GetComponent<EnemySpawner>().spawnEnemy();
                }
            }

            enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            print(enemyList.Count());

            if (enemyList.Count() != 0)
            {
                activeEnemy = true;
                foreach (GameObject door in doorList)
                {
                    door.GetComponent<Door>().close();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentRoom = false;
            foreach (GameObject enemy in enemyList)
            {
                Destroy(enemy);
            }
            doorList = new List<GameObject>();
            enemyList = new GameObject[0];
            spawnerList = new List<GameObject>();
        }
    }

    private bool aliveEnemy()
    {
        for (int i = 0; i < room.transform.childCount; i++)
        {
            GameObject[] tempEnemyList = GameObject.FindGameObjectsWithTag("Enemy");
            if (tempEnemyList.Count() > 0)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        if (aliveEnemy() == false && activeEnemy == true)
        {
            activeEnemy = false;
            foreach (GameObject door in doorList)
            {
                door.GetComponent<Door>().open();
            }
            nextFloorObject.GetComponent<NextFloorTeleporter>().setActive();
        }
    }
}
