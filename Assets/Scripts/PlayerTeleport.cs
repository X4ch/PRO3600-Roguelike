using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            if (collision.gameObject == currentDoor)
            {
                currentDoor = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDoor != null)
        {
            transform.position = currentDoor.GetComponent<Door>().GetDestination().position;
        }
    }
}
