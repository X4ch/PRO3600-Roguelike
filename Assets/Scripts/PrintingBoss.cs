using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PrintingBoss : EnemyObject
{

    public GameObject boss;
    public int patern;
    public float paternDuration;
    public GameObject enemy1prefab;
    public GameObject enemy2prefab;
    private Vector2 spawmPosition;
    private int enemycount;

    //ce boss va invoquer entre 1 et 2 ennemis, avec un maximum de 4 sur le terrain, toute les 3 secondes, puis se téléporte à une position aléatoire de la salle
    protected override void Move()
    {
        isBoss = true;
        enemycount = GameObject.FindGameObjectsWithTag("Enemy").Length -1; //vérifie le nombre d'ennemi qu'il a invoqué pour éviter d'en avoir trop
        animator.SetBool("ischarging", false); 
        if (currentHealth < maxHealth / 3) //si il a moins de 1/3 pv il change de pattern
        {
            patern = 3;
        }
        else if (currentHealth < 2*maxHealth / 3) //si il a moins de 2/3 pv il change de pattern
        {
            patern = 2;
        }
        else
        {
            patern = 1;
        }
        paternDuration -= Time.deltaTime;
        if (enemycount < 3) 
        {
            if (paternDuration <= 0) { animator.SetBool("ischarging", true); paternDuration = 4; } //si il peut invoquer un ennemi, il va lancer l'animation de chargement, puis lancer un timer de 3 secondes après laquelle un enneis sera invoqué
                
            if (paternDuration <= 1){ Patern(); paternDuration = -1; }
        }
            
        
    }

    //définit les diff?rents patternes possibles
    void Patern()
    {
        
        
            
            spawmPosition = boss.transform.position;
            if (patern == 1) //invoque un firstenemy
            {

                    GameObject enemy = Instantiate(enemy1prefab, spawmPosition, new Quaternion(0, 0, 0, 0));
                    transform.position = new Vector2(Random.Range(0, 10) - 5 + currentRoom.transform.position.x, Random.Range(0, 6) - 3 + currentRoom.transform.position.y);

            }

            else if (patern == 2) //invoque 2 firstennemy
            {

                    GameObject enemy1 = Instantiate(enemy1prefab, spawmPosition, new Quaternion(0, 0, 0, 0));
                    GameObject enemy2 = Instantiate(enemy1prefab, spawmPosition, new Quaternion(0, 0, 0, 0));
                    transform.position = new Vector2(Random.Range(0, 10) - 5 + currentRoom.transform.position.x, Random.Range(0, 6) - 3 + currentRoom.transform.position.y);


            }
            else if (patern == 3) //invoque 1 firstennemy et 1 enrraging ennemy
            {

                    GameObject enemy1 = Instantiate(enemy1prefab, spawmPosition, new Quaternion(0, 0, 0, 0));
                    GameObject enemy2 = Instantiate(enemy2prefab, spawmPosition, new Quaternion(0, 0, 0, 0));
                    transform.position = new Vector2(Random.Range(0, 10) - 5 + currentRoom.transform.position.x, Random.Range(0, 6) - 3 + currentRoom.transform.position.y);


            
            
        }
        
        
    }
        


}
