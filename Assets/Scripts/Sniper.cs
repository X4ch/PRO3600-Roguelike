using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : EnemyObject
{
    private float timerEnemy = -1;
    public float speed = 20f;
    public Transform firePoint;
    private Quaternion rot;
    public GameObject bulletPrefab;
    protected override void Move()
    {
        firePoint = rgbd.transform;
        if (timerEnemy > 0) //le timer est de 1 seconde, chaques seconde, il tire un projectile
        {
            Vector2 playerPos = target.position;

            float deltax = (playerPos.x - firePoint.position.x);
            float deltay = (playerPos.y - firePoint.position.y);
            float sumdelta = deltax * deltax + deltay * deltay; //de la même manire que le calcul de déplacement de firstennemy ou berserker, le vecteur entre les position de sniper et du joueur est calculé
            float dist = Mathf.Sqrt(sumdelta);
            //rot = Mathf.Atan(deltay / deltax) * 180 / Mathf.PI + 90;
            timerEnemy = -1;
            //Debug.Log(deltax);


            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, new Quaternion(deltax/dist, deltay/dist, 0, 0)); //une nouvelle balle est instancié à la position du sniper et en direction en joueur (on se sert du vecteur rotation pour envoyer les données de direction)
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * speed, ForceMode2D.Impulse);
        }
        else
        {
            timerEnemy += Time.deltaTime; ;
        }
    }

}
