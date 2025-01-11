using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    // Déclaration des variables utiles pour le script
    public int currentFloor = 0;

    // Ces deux listes contiennent le nombre mionimum et maximum de salle que chaque étage peux avoir
    private int[] minRoomNumber = {7, 8, 10, 11, 16, 17, 21};
    private int[] maxRoomNumber = {9, 10, 15, 16, 20, 21, 30};
    public bool generateNext;
    public FloorGenerator floorGenerator;
    public MiniMapManager miniMapManager;

    // Fonction qui permet de supprimer toutes les salles présente en vue de générer l'étage suivant
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
    
    // Cette fonction permet de générer un étage en faisant appelle au floorGenerator et elle prend également en charge ce qu'il doit ce passer lorsque le joueurà finit les 7 étages du jeu
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

    // Permet de générer le première étage au démarage du jeu
    private void Start()
    {
        miniMapManager = GameObject.FindGameObjectsWithTag("MiniMapManager")[0].GetComponent<MiniMapManager>();
        floorGenerator = GameObject.FindObjectOfType<FloorGenerator>();
        generateNextFloor();
    }

    // Fonction utilisé par le téléporteur pour passer à l'étage suivant
    public void nextFloor()
    {
        generateNextFloor();
    }

    // Fonction qui permet de générer la salle de fin du jeu (endRoom)
    private void generateEndRoom()
    {
        miniMapManager.canvas.SetActive(false);
        clearFloor();
        floorGenerator.generateEndRoom();
    }
}
