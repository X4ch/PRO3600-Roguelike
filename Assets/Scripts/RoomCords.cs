using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomCords : MonoBehaviour
{
    public bool currentRoom;
    public bool activeEnemy;
    public bool asBeenUpdated;

    // Diff�rents GameObject permettant de bien faire fonctionner la salle
    public GameObject roomGameObject;
    public List<GameObject> doorList;
    public GameObject[] enemyList;
    public List<GameObject> spawnerList;
    public GameObject nextFloorObject = null;

    public Room room;
    public MiniMapManager miniMapManager;
    public PickUpSpawner pickUpSpawner;

    [SerializeField] private Transform roomCords;

    public Transform GetPosition()
    {
        return roomCords;
    }

    // Cette fonction permet que lorsque le joueur entre dans une salle les ennemies qui si trouve apparaisent, que les portes se ferme si il y a des ennemies

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentRoom = true;
            miniMapManager = GameObject.FindGameObjectWithTag("MiniMapManager").GetComponent<MiniMapManager>();
            if (!asBeenUpdated)
            {
                miniMapManager.updateRoomStatus(room);
                asBeenUpdated = true;
            }
            

            for (int i = 0; i < roomGameObject.transform.childCount; i++)
            {  
                if (roomGameObject.transform.GetChild(i).tag == "Door")
                {
                    doorList.Add(roomGameObject.transform.GetChild(i).gameObject);
                }

                if (roomGameObject.transform.GetChild(i).tag == "Spawner")
                {
                    spawnerList.Add(roomGameObject.transform.GetChild(i).gameObject);
                }
                if (roomGameObject.transform.GetChild(i).tag == "NextFloor")
                {
                    nextFloorObject = roomGameObject.transform.GetChild(i).gameObject;
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

    // Cette fonction permet de mettre � z�ro les diff�rentes variables qu'une salle utilise lorsqu'elle est active afin d'�v�iter tout probl�me. Elle est appel� lorsuqe le joueur quitte la salle.
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

    // Permet de v�rifier la pr�sence d'ennemies en vie dans la salle, utilis� pour savoir quand r�ouvrir les portes
    private bool aliveEnemy()
    {
        for (int i = 0; i < roomGameObject.transform.childCount; i++)
        {
            GameObject[] tempEnemyList = GameObject.FindGameObjectsWithTag("Enemy");
            if ( tempEnemyList.Count() > 0)
            {
                return true;
            }
        }
        return false;
    }
    
    // Cette fonction permet de r�ouvrir les portes de la salle lorque que tout les ennemies ont �t� tuer, elle permet aussi de faire apparaitre un consommable enf aisant appel au script pickUpGenerator.
    private void Update()
    {
        if (aliveEnemy() == false && activeEnemy == true)
        {
            activeEnemy = false;
            spawnerList = new List<GameObject>();
            foreach (GameObject door in doorList)
            {
                door.GetComponent<Door>().open();
            }
            // Permet d'activer le t�l�porteur vers le prochaine �tage lorsque tout les ennemies/ boss ont �t� tuer dans le salle de fin d'�tage
            if (nextFloorObject != null)
            {
                nextFloorObject.GetComponent<NextFloorTeleporter>().setActive();
            }
            if (pickUpSpawner != null)
            {
                pickUpSpawner.generatePickUp();
            }
        }
    }
}
