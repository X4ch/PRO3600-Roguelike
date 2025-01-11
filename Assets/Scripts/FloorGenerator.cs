using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FloorGenerator : MonoBehaviour
{

    public Transform FloorGeneratorTransform;

    public Transform[] DoorGeneratorsTransform;

    public GameObject[] Doors;

    private Transform playerTransform;

    public float scale;
    public int seed;

    public GameObject[] spawnRoomList;
    public GameObject[] baseRoomList;
    public GameObject[] bossRoomList;
    public GameObject[] itemRoomList;

    public GameObject endRoom;

    private System.Random rng = new System.Random();

    //Permet de générer une salle dans le jeu
    // Le paramètre type correspond au type de la salle : 1 pour la salle du début de l'étage, 2 pour des salles quelconque,
    // 3 pour une salle de fin d'étage et 4 pour une salle d'objet 
    private GameObject generateRoom(int roomType)
    {
        GameObject[] tempRoomList;
        if (roomType == 1)
        {
            tempRoomList = spawnRoomList;
            int test = rng.Next(0, tempRoomList.Length);
            GameObject currentRoom = Instantiate(tempRoomList[test], FloorGeneratorTransform.position, quaternion.identity);
            return currentRoom;
        }
        else if (roomType == 2)
        {
            tempRoomList = baseRoomList;
            int test = rng.Next(0, tempRoomList.Length);
            GameObject currentRoom = Instantiate(tempRoomList[test], FloorGeneratorTransform.position, quaternion.identity);
            return currentRoom;
        }
        else if (roomType == 3)
        {
            tempRoomList = bossRoomList;
            int test = rng.Next(0, tempRoomList.Length);
            GameObject currentRoom = Instantiate(tempRoomList[test], FloorGeneratorTransform.position, quaternion.identity);
            return currentRoom;
        }
        else
        {
            tempRoomList = itemRoomList;
            int test = rng.Next(0, tempRoomList.Length);
            GameObject currentRoom = Instantiate(tempRoomList[test], FloorGeneratorTransform.position, quaternion.identity);
            return currentRoom;
        }
    }

    //Permet de générer les portes d'une salle
    private void generateDoors(GameObject currentRoom, int i, int j, Floor floor)
    {
        for (int k = 0; k < 4; k++)
        {
            // Génère les portes comme enfant de la salle à laquelle elle appartient
            if (floor.floor[i, j].neighbor[k] == true)
            {
                Instantiate(Doors[k], DoorGeneratorsTransform[k].position, quaternion.identity, currentRoom.transform);
            }
        }
    }

    public void generateFloor(Floor floor)
    {
        playerTransform = GameObject.FindWithTag("Player").transform;

        int floorSize = floor.getDim();
        float newx;
        float newy;
        float z = FloorGeneratorTransform.position.z;

        // On itère sur la matrice de l'étage afin de le génerer
        for (int i = 0; i < floorSize; i++)
        {
            newy = -(i * scale);
            for (int j = 0; j < floorSize; j++)
            {
                newx = j * scale;
                FloorGeneratorTransform.position = new Vector3(newx, newy, z);
                if (floor.floor[i, j].type != 0)
                {
                    GameObject currentRoom = generateRoom(floor.floor[i, j].type);
                    generateDoors(currentRoom, i, j, floor);
                    currentRoom.GetComponent<RoomCords>().room = floor.floor[i, j];
                    if (floor.floor[i, j].type == 1)
                    {
                        playerTransform.position = FloorGeneratorTransform.position;
                    }

                }

            }
            FloorGeneratorTransform.position.Set(0, newy, FloorGeneratorTransform.position.z);
        }
        print(floor.toString());
    }

    // Fonction qui permet de générer la salle de fin du jeu
    public void generateEndRoom()
    {
        FloorGeneratorTransform.position = Vector3.zero;
        playerTransform.position = Vector3.zero;
        Instantiate(endRoom, FloorGeneratorTransform.position, Quaternion.identity);
        Room endRoomRoom = new Room(8, 0, 0, 5, "end", 0, "VI");
        endRoom.GetComponent<RoomCords>().room = endRoomRoom;

    }
}
