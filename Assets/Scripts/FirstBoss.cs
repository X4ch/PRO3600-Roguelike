using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class FirstBoss : EnemyObject
{
    private float speed = 10;
    private float timerEnemy = -1;
    private float sleep = 3;
    private float deltax = 1;
    private float deltay = 1;
    private float sumdelta = 1;
    private float dist = 1;

    public int patern;
    public float paternDuration;
    
    float totdist;
    int choice;
    Vector2 end;
    Vector2 playerPos;
    Vector2 selfPos;

    //le "move" du boss va avoir 3 patterns diff�rents qui vont durer entre 5 et 10 secondes et sont selectionn�s de mani�re al�atoire
    protected override void Move()
    {
        isBoss = true;
        patern = 3;
        if (paternDuration + timerEnemy < -1) { choice = 2 /*Random.Range(0, patern)*/; sumdelta = 1 ; paternDuration = Random.Range(5, 10); is_hittable = true; animator.SetBool("isdashing", false);
            animator.SetBool("isrunning", false);
            animator.SetBool("isjumping", false);
        }
        paternDuration -= Time.deltaTime;
        Patern();
    }

    //d�finit les diff�rents patternes possibles
    void Patern()
    {
        //ce premier pattern fait foncer le boss en ligne droite vers le joueur, le joueur a un leger temps de pause, le boss �tant en train de charger son attaque
        if (choice == 0)
        {
            
            Vector2 selfPos = this.gameObject.transform.position;
            // la position vers lequel le boss va foncer est d�finit ici
            if (timerEnemy < 0 && sleep < 0 && paternDuration > 0)
            {
                sleep = 3;
                timerEnemy = Random.Range(0.5f, 2);
                end = target.position;
                deltax = (end.x - selfPos.x);
                deltay = (end.y - selfPos.y);
                sumdelta = deltax * deltax + deltay * deltay;
                dist = Mathf.Sqrt(sumdelta);

            }
            //dur�e pendant lequel le boss tourne (pour indiquer au jouer o� il va foncer), cette action dure 3 secondes (variable sleep)
            else if (sleep > 0)
            {
                sleep -= Time.deltaTime;
                if (deltax > 0)
                {
                    rgbd.velocity = new Vector2(0.001f, 0);
                }
                else
                {
                    rgbd.velocity = new Vector2(-0.001f, 0);
                }
                rgbd.MoveRotation((Mathf.Atan(deltay / deltax) * 180 / Mathf.PI + 90));
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                animator.SetBool("isdashing", true);
            }
            // le boss fonce pendant la dur�e timerEnemy (entre 0.5 et 2 secondes)
            else
            {
                timerEnemy -= Time.deltaTime;



                rgbd.velocity = new Vector2(deltax * (speed * 4) / dist, deltay * (speed * 4) / dist);
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                animator.SetBool("isdashing", false);
            }
        }
        // le boss se dirige vers le joueur pendant toute la dur�e du pattern
        else if (choice == 1)
        {
            animator.SetBool("isrunning", true);
            Vector2 playerPos = target.position;
            Vector2 selfPos = this.gameObject.transform.position;
            float deltax = (playerPos.x - selfPos.x);
            float deltay = (playerPos.y - selfPos.y);
            float sumdelta = deltax * deltax + deltay * deltay;
            dist = Mathf.Sqrt(sumdelta);

            rgbd.velocity = new Vector2(deltax * speed / (2 * dist), deltay * speed / (2 * dist));
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
        }
        // le boss fait des bonds vers le joueur (la partie "bond" sera faite en animation)
        else if (choice == 2)
        {
            // la position vers lequel le boss va sauter est d�finit ici
            
            if (sumdelta <= 1 && paternDuration > 0 && sleep < 0)
            {
                
                sleep = 2;
                playerPos = target.position;
                selfPos = this.gameObject.transform.position;
                deltax = (playerPos.x - selfPos.x);
                deltay = (playerPos.y - selfPos.y);
                sumdelta = deltax * deltax + deltay * deltay;
                totdist = Mathf.Sqrt(sumdelta);
                is_hittable = false; // sa hitbox est d�sactiv�e durant le saut
            }
            else if (sumdelta <= 1) { is_hittable = true; deltax = 0; deltay = 0; } //une fois le saut effectu�, la hitbox est r�activ�e
            sleep -= Time.deltaTime;
            animator.SetBool("isjumping", false);
            //le boss saute pendant une dur�e fixe peut importe la distance (vitesse plus rapide en fonction de la distance)
            if (sumdelta > 1) 
            {
                animator.SetBool("isjumping", true);
                selfPos = this.gameObject.transform.position;
                deltax = (playerPos.x - selfPos.x);
                deltay = (playerPos.y - selfPos.y);
                sumdelta = deltax * deltax + deltay * deltay;
                dist = Mathf.Sqrt(sumdelta);
            }

            rgbd.velocity = new Vector2(deltax * speed / (2 * dist) * totdist/4, deltay * speed / (2 * dist)* totdist/4);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
        }
        //si aucun patern n'est actif le boss s'arrete
        else
        {
            animator.SetBool("isdashing", false);
            animator.SetBool("isrunning", false);
            animator.SetBool("isjumping", false);
            rgbd.velocity = new Vector2(0, 0);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
        }
        
    }


}
