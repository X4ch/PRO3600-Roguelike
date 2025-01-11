using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arme : MonoBehaviour
{
    public float speed = 20f;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject currentRoom;
    public Camera cam;
    public PlayerV2 player;

    Vector2 mousePos;

    // initialise la currentroom du bullet
    private void OnTriggerEnter2D(Collider2D collision)  
    {
        if (collision.CompareTag("Room"))
        {
            currentRoom = collision.gameObject;

        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerV2>(); // recupere le gameobject player dans le prefab
    }

    //supprime la valeur assigne a currentroom quand on change de room
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
    

        // Update is called once per frame
        //a chaque frame quand on tire on perd de la mana et on appelle la fonction shoot
        void Update()
        {
            if (Input.GetMouseButtonDown(1) && player.currentMana > 0)
            {
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition,0);

                Vector2 roomPosition = currentRoom.GetComponent<RoomCords>().GetPosition().position;

                player.currentMana -=1;
                Shoot();

            }
        
    }

    //permet de tirer les projectiles(bullets) dans la bonne direction (celle de la souris)
    void Shoot ()
    {
        
        float deltax = (transform.position.x - currentRoom.transform.position.x);  // recupere les coordonnees du vecteur direciton de tir
        float deltay = (transform.position.y - currentRoom.transform.position.y);
        float sumdelta = deltax * deltax + deltay * deltay;
        float dist = Mathf.Sqrt(sumdelta);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, new Quaternion((-deltax/8.895f + mousePos.x)*1.779f,(-deltay/4.95f + mousePos.y)*0.99f,0,0));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up*speed, ForceMode2D.Impulse);
    }
}  
