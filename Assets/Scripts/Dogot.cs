using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Dogot : EnemyObject
{
    private string smellFound;
    private float timerEnemy = -1;
    Vector2 end;
    private float speed = 5f;
    private Vector2 nextsmell;
    private float deltax;
    private float deltay;
    private float sumdelta;
    private float dist;
    
    // ce programme se sert du tableau "smell" de player V2, qui stocke les position du joueur les 14 dernières secondes passées
    protected override void Move()
    {
        Vector2 selfPos = this.gameObject.transform.position;
        Vector2 playerPos = target.position;
        deltax = (playerPos.x - selfPos.x);
        deltay = (playerPos.y - selfPos.y);
        sumdelta = deltax * deltax + deltay * deltay;
        dist = Mathf.Sqrt(sumdelta);
        if (dist < 4) //si le joueur est assez proche, il le détecte 
        {
            smellFound = "playerFound";
        }
        else { //sinon dogot vérifie si un élément du tableau smell est assez zproche de lui pour être détecté
            if (smellFound != "smellFound")
            {
                foreach (Vector2 ele in player.smell)
                {

                        float deltax = (ele.x - selfPos.x);
                        float deltay = (ele.y - selfPos.y);
                        float sumdelta = deltax * deltax + deltay * deltay;
                        float dist = Mathf.Sqrt(sumdelta);

                        if (dist < 2)
                        {
                            smellFound = "smellFound"; //si une odeur est detectée, elle est enregistrée pour ettre utilisé après
                            nextsmell = ele;
                            break;
                        }
                }

            }
            
        }
        
        if (smellFound == null)
        {
            

            //choisit une position aléatoire
            if (timerEnemy < 0)
            {
                timerEnemy = Random.Range(1, 3);
                end = new Vector2(Random.Range(0, 10) - 5 + currentRoom.transform.position.x, Random.Range(0, 6) - 3 + currentRoom.transform.position.y); 

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
        else if (smellFound == "smellFound") //si l'odeur est trouvée, il va alors courrir en remontant la piste
        {
            deltax = (nextsmell.x - selfPos.x);
            deltay = (nextsmell.y - selfPos.y);
            sumdelta = deltax * deltax + deltay * deltay;
            dist = Mathf.Sqrt(sumdelta);

            rgbd.velocity = new Vector2(deltax * (speed *2) / dist, deltay * (speed *2) / dist); //pour cela, il va d'abord courir vers la position précédement detectée
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            
            if (dist < 1)
            {
                
                for (int i = 1; i< player.smell.Length;i++) //puis il va récuperer la position suivante de la liste. L'intervalle de temps entre chaque position pour que le chien ne se retrouve pas sans trace
                {
                    if (player.smell[i] == nextsmell)
                    {
                        nextsmell = player.smell[i - 1];
                        break;
                    }
                }
            }
            
        }
        else if (smellFound == "playerFound") //si le joueur est trouvé, dogot coure vers lui
        {
            deltax = (playerPos.x - selfPos.x);
            deltay = (playerPos.y - selfPos.y);
            sumdelta = deltax * deltax + deltay * deltay;
            dist = Mathf.Sqrt(sumdelta);

            rgbd.velocity = new Vector2(deltax * (speed * 2) / dist, deltay * (speed * 2) / dist);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
