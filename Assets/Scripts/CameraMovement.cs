using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{

    private Transform cameraTransform;
    private GameObject currentRoom;

    public void Start()
    {
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;
    }
    //Permet de détecter dans  quel salle se trouve le joueur
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            currentRoom = collision.gameObject;
            //Debug.Log("Collision détecté");    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            if (collision.gameObject == currentRoom)
            {
                currentRoom = null;
            }
        }
    }

    // Actualise le placement de la caméra en fonction de la salle dans laquelle se trouve le joueur
    void Update()
    {
        if (currentRoom != null)
        {
            Vector3 position = currentRoom.GetComponent<RoomCords>().GetPosition().position;
            position.z = -10;
            cameraTransform.position = position;
        }
    }

}
