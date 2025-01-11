using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    // declaration des variables utiles pour le programme
    public float speed = 20f;
    
    public Camera cam;
    public Rigidbody2D rbPlayer;
    public Rigidbody2D rbBullet;
    public GameObject firePoint;
    
    int Damage = 15;

    Vector2 mousePos;
   

    // Start is called before the first frame update
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // note la position de la souris sur l'ecran de jeu
        
    }

    void FixedUpdate()
    {
        //Vector2 roomPosition = currentRoom.GetComponent<RoomCords>().GetPosition().position;
        //Vector2 lookDir = mousePos - rbPlayer.position + roomPosition;
        //Debug.Log( rbPlayer.transform.position);
        rbBullet.velocity = new Vector2(transform.rotation.x * speed, transform.rotation.y * speed); // applique une vitesse sur les projectiles pour qu'ils se deplacent
    }


    // S'execute quand les projectiles rencontrent des objets/enemies
    void OnTriggerEnter2D (Collider2D hitTarget)
    {

        EnemyObject enemy = hitTarget.GetComponent<EnemyObject>();   

        if (enemy != null)                                       // permet d'appliquer des degats aux enemies
        {
            enemy.TakeDamage(Damage);
        }
        

        if (hitTarget.GetComponent<EnemyObject>())
        {
            Destroy(gameObject);
        }
        else if (hitTarget.CompareTag("Wall")) 
        {
            Destroy(gameObject);
        }

        
    }

}
