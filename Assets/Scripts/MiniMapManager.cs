using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapManager : MonoBehaviour
{
    public Floor currentFloor;
    public FloorManager floorManager;
    public GameObject canvas;
    public PlayerV2 player;

    public Color spawnRoomColor;
    public Color roomColor;
    public Color bossRoomColor;
    public Color itemRoomColor;


    public void updateRoomStatus(Room room) //Met a jour le statut de la salle de vue a visitee
    {
        Debug.Log(room.toString());
        if (floorManager.currentFloor < 8)
        {
            room.roomStatus = "VI";
            colorUpdate(room);

            for (int i = 1; i < 5; i++)
            {
                if (currentFloor.floor[currentFloor.updateCords((room.x, room.y), i).Item1, currentFloor.updateCords((room.x, room.y), i).Item2].roomStatus == "NS")
                {
                    currentFloor.floor[currentFloor.updateCords((room.x, room.y), i).Item1, currentFloor.updateCords((room.x, room.y), i).Item2].roomStatus = "SE";
                    colorUpdate(currentFloor.floor[currentFloor.updateCords((room.x, room.y), i).Item1, currentFloor.updateCords((room.x, room.y), i).Item2]);
                }
            }
        }
    }


    public void colorInit() //Change la couleur de la salle sur la minimap en fonction de son type
    {
        for (int i = 0; i < currentFloor.getDim(); i++)
        {
            for (int j = 0; j < currentFloor.getDim(); j++)
            {
                string name = "(" + i + "," + j + ")";
                Room room = currentFloor.floor[i, j];
                switch (room.type)
                {
                    case 1:
                        GameObject.Find(name).GetComponent<Image>().color = spawnRoomColor; break;

                    case 2:
                        GameObject.Find(name).GetComponent<Image>().color = roomColor; break;

                    case 3:
                        GameObject.Find(name).GetComponent<Image>().color = bossRoomColor; break;

                    case 4:
                        GameObject.Find(name).GetComponent<Image>().color = itemRoomColor; break;
                    default:
                        break;
                }
            }
        }
    }

    void colorUpdate(Room room) //Change l'opacite de la salle sur la carte selon son statut
    {   
        string name = "(" + room.x + "," + room.y + ")";
        Color color = GameObject.Find(name).GetComponent<Image>().color;
        if (room.roomStatus == "SE") { color.a = 0.5f; GameObject.Find(name).GetComponent<Image>().color = color; }

        else if (room.roomStatus == "VI") { color.a = 1f; GameObject.Find(name).GetComponent<Image>().color = color; }

        else { }

        }

    // Permet d'assigner différents éléments nécessaire au bon fonctionnement du programme au moment du lancement du jeu
    private void Start()
    {
        floorManager = GameObject.FindGameObjectsWithTag("FloorManager")[0].GetComponent<FloorManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerV2>();
    }

    public void resetMap() //Reinitialise la carte apres la completion d'un etage
    {
        for (int i= 0; i < currentFloor.getDim(); i++)
        {
            for (int j = 0; j < currentFloor.getDim(); j++)
            {
                string name = "(" + i + "," + j + ")";
                Color color = Color.white;
                color.a = 0f;
                Debug.Log(name);
                GameObject.Find(name).GetComponent<Image>().color = color;
            }
        } 
    }

}
