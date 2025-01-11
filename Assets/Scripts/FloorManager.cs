using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    // D�claration des variables utiles pour le script
    public int currentFloor = 0;

    // Ces deux listes contiennent le nombre mionimum et maximum de salle que chaque �tage peux avoir
    private int[] minRoomNumber = {7, 8, 10, 11, 16, 17, 21};
    private int[] maxRoomNumber = {9, 10, 15, 16, 20, 21, 30};
    public bool generateNext;
    public FloorGenerator floorGenerator;
    public MiniMapManager miniMapManager;

    // Fonction qui permet de supprimer toutes les salles pr�sente en vue de g�n�rer l'�tage suivant
    void clearFloor() {
        GameObject[] roomList = GameObject.FindGameObjectsWithTag("Room");
        if (roomList.Length > 0)
        {
            foreach (GameObject room in roomList)
            {
                Destroy(room);
            }
        }
        GameObject[] pickUpList = GameObject.FindGameObjectsWithTag("PickUp");
        if (pickUpList.Length > 0)
        {
            foreach(GameObject pickUp in pickUpList)
            {
                Destroy(pickUp);
            }
        }
    }
    
    // Cette fonction permet de g�n�rer un �tage en faisant appelle au floorGenerator et elle prend �galement en charge ce qu'il doit ce passer lorsque le joueur� finit les 7 �tages du jeu
    public void generateNextFloor()
    {
        if (currentFloor < 7)
        {
            clearFloor();
            Floor floor = new Floor(13, "", currentFloor);
            miniMapManager.currentFloor = floor;
            floor.generateFloor(2, minRoomNumber[currentFloor], maxRoomNumber[currentFloor]);
            floorGenerator.generateFloor(floor);
            currentFloor++;
            miniMapManager.resetMap();
            miniMapManager.colorInit();
        }
        else
        {
            currentFloor++;
            generateEndRoom();
        }
    }

    // Permet de g�n�rer le premi�re �tage au d�marage du jeu
    private void Start()
    {
        miniMapManager = GameObject.FindGameObjectsWithTag("MiniMapManager")[0].GetComponent<MiniMapManager>();
        floorGenerator = GameObject.FindObjectOfType<FloorGenerator>();
        generateNextFloor();
    }

    // Fonction utilis� par le t�l�porteur pour passer � l'�tage suivant
    public void nextFloor()
    {
        generateNextFloor();
    }

    // Fonction qui permet de g�n�rer la salle de fin du jeu (endRoom)
    private void generateEndRoom()
    {
        miniMapManager.canvas.SetActive(false);
        clearFloor();
        floorGenerator.generateEndRoom();
    }
}
