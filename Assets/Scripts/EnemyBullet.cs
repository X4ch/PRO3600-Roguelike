using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;

    public Sniperteleporter owner;
    public Sniperboss owner2;
    public Rigidbody2D rbBullet;
    public GameObject firePoint;

    int Damage = 5;

    //pour mieux comprendre ce code, il vaut mieux commencer par regarder un des codes sniperboss, sniper, ou sniperteleporter
    //ce code g�re les balles des ennemis


    void FixedUpdate()
    {
        //Vector2 roomPosition = currentRoom.GetComponent<RoomCords>().GetPosition().position;
        //Vector2 lookDir = mousePos - rbPlayer.position + roomPosition;

        //� chaque mise � jour du jeu, on met la vitesse au vecteurs donn�e par le tireur afin d'atteindre la position du joueur � l'instant du tir 
        rbBullet.velocity = new Vector2(transform.rotation.x * speed, transform.rotation.y * speed);

    }



    void OnTriggerEnter2D(Collider2D hitTarget)
    {
        Sniperteleporter enemy = hitTarget.GetComponent<Sniperteleporter>();
        Sniperboss enemy2 = hitTarget.GetComponent<Sniperboss>();

        if (enemy != null || enemy2 != null)
        {
            
            
            owner = enemy;
            owner2 = enemy2;
            
        }
        if (hitTarget.CompareTag("Player")) //si le joueur est touch� il prend des d�gats
        {
            hitTarget.GetComponent<PlayerV2>().GetHit(Damage);
            Destroy(gameObject);
        }

        else if (hitTarget.CompareTag("Wall")) //si le mur est touch� la balle est d�truite (pour �viter d'avoir trop de clone)
        {
            Destroy(gameObject);
        }


    }

    private void OnDestroy() 
    {
        if (owner != null) //si l'ennemi est sniperteleporter, il va se t�l�porter � l'emplacement o� la balle se d�truit (avec une l�g�re correction pour �viter qu'il apparaisse dans un mur)
        {
            owner.Teleport(transform.position.x  - transform.rotation.x , transform.position.y - transform.rotation.y   );
        }
        if (owner2 != null)//si l'ennemi est sniperboss, il va se cr�er un sniper � l'emplacement o� la balle se d�truit (avec une l�g�re correction pour �viter qu'il apparaisse dans un mur)
        {
            owner2.CreateSniper(transform.position.x - transform.rotation.x  , transform.position.y - transform.rotation.y );
        }
    }

}
