using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketBeast : EnemyObject
{
    private float speed = 15;
    private float timerEnemy = -1;
    private float sleep = 3;
    private float deltax=1;
    private float deltay=1;
    private float sumdelta=1;
    private float dist=1;

    Vector2 end;
    //move fait foncer l'ennemi en ligne droite vers une position aléatoire dans la salle
    protected override void Move()
    {
        Vector2 selfPos = this.gameObject.transform.position;
        // définition de la position aléatoire où il va foncer,le joueur a un leger temps de pause, l'ennemi étant en train de charger son attaque
        if (timerEnemy < 0 && sleep <0)
        {
            sleep = 3;
            timerEnemy = Random.Range(0.5f, 2);
            end = new Vector2(Random.Range(0, 10) - 5 + currentRoom.transform.position.x, Random.Range(0, 6) - 3 + currentRoom.transform.position.y); //mettre dim salle
            deltax = (end.x - selfPos.x);
            deltay = (end.y - selfPos.y);
            sumdelta = deltax * deltax + deltay * deltay;
            dist = Mathf.Sqrt(sumdelta);
            

        }
        //l'ennemi de tourne en 3 secondes
        else if (sleep > 0)
        {
            if (deltax > 0)
            {
                rgbd.velocity =new Vector2( 0.001f , 0);
            }
            else
            {
                rgbd.velocity = new Vector2(-0.001f, 0);
            }
            sleep -= Time.deltaTime;
            rgbd.MoveRotation((Mathf.Atan(deltay/deltax) * 180/Mathf.PI));
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            animator.SetBool("ischarging", true);        
            
        }
        //l'ennemi fonce pendant entre 0.5 et 2 secondes
        else
        {
            timerEnemy -= Time.deltaTime;
            
            

            rgbd.velocity = new Vector2(deltax * (speed) / dist, deltay * (speed) / dist);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            animator.SetBool("ischarging", false);
        }
    }
}
