using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoom : MonoBehaviour
{
    [SerializeField] private Transform roomCords;
    public Transform GetPosition()
    {
        return roomCords;
    }
}
