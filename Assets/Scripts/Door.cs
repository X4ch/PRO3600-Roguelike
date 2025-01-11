using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform destination;
    public BoxCollider2D doorCollider;
    public bool isOpen = true;
    public Animator animator;

    public Transform GetDestination()
    {
        return destination;
    }

    public void close()
    {
        doorCollider.enabled = false;
        isOpen = false;
        animator.SetBool("isclosed", true);
    }

    public void open()
    {
        doorCollider.enabled = true;
        isOpen = true;
        animator.SetBool("isclosed", false);
    }

}
