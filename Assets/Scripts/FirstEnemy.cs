using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FirstEnemy : EnemyObject
{
    private float speed = 5;
    private float timerEnemy = -1;
    Vector2 end;
    // Update is called once per frame

    //un ennemi qui va se déplacer aléatoirement jusqu'à trouver un joueur; une fois détecté, le joueur ne peux plus s'en echapper
    protected override void Move()
    {
        
        
        Vector2 playerPos = target.position;
        Vector2 selfPos = this.gameObject.transform.position;
        float deltax = (playerPos.x - selfPos.x);
        float deltay = (playerPos.y - selfPos.y);
        float sumdelta = deltax * deltax + deltay * deltay;
        float dist = Mathf.Sqrt(sumdelta); //calcule la distance de berserker au joueur
        //annonce que le joueur est detecté
        if (dist < 10)
        {
            detectarget = true;
        }
        //ce if permet d'empecher un bug lorsqu'il est trop proche du joueur
        if (dist > 0.7 && detectarget)
        {
            rgbd.velocity = new Vector2(deltax * speed / dist, deltay * speed / dist);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
        }
        else if (!detectarget)
        {
            //choisit une position aléatoire
            if (timerEnemy < 0)
            {
                timerEnemy = Random.Range(1, 3);
                Debug.Log(currentRoom);
                end = new Vector2(Random.Range(0, 10) - 5 + currentRoom.transform.position.x, Random.Range(0, 6) - 3 + currentRoom.transform.position.y); //mettre dim salle

            }
            //se déplace vers elle pendant la durée timerEnemy
            else 
            {
                timerEnemy -= Time.deltaTime;
                deltax = (end.x - selfPos.x);
                deltay = (end.y - selfPos.y);
                sumdelta = deltax * deltax + deltay * deltay;
                dist = Mathf.Sqrt(sumdelta);

                rgbd.velocity = new Vector2(deltax * (speed / 4) / dist, deltay * (speed / 4) / dist);
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
            }
            
        }
        //ce if permet d'empecher un bug lorsqu'il est trop proche de sa position definie
        else if (dist < 0.7)
        {
            rgbd.velocity = new Vector2(0,0);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
        }


    }

    




}
