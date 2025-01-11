using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniperboss : EnemyObject
{
    private float timerEnemy = -1;
    public float speed = 20f;
    public Transform firePoint;
    private Quaternion rot;
    public GameObject bulletPrefab;
    public GameObject sniperPrefab;

    //voir le code de Sniper pour le corps principal
    protected override void Move()
    {
        isBoss = true;
        firePoint = rgbd.transform;
        if (timerEnemy > 0)
        {
            Vector2 playerPos = target.position;

            float deltax = (playerPos.x - firePoint.position.x);
            float deltay = (playerPos.y - firePoint.position.y);
            float sumdelta = deltax * deltax + deltay * deltay;
            float dist = Mathf.Sqrt(sumdelta);
            //rot = Mathf.Atan(deltay / deltax) * 180 / Mathf.PI + 90;
            timerEnemy = -1;
            //Debug.Log(deltax);


            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, new Quaternion(deltax / dist, deltay / dist, 0, 0));
            if (currentHealth < 50) // si l'ennemi a moins de 50% de point de vie, il tire 2 balle suplémentaires (car il a 2 yeux en plus)
            {
                animator.SetBool("islow", true);
                GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position + new Vector3(1.5f, 0, 0), new Quaternion(deltax / dist, deltay / dist, 0, 0));
                GameObject bullet3 = Instantiate(bulletPrefab, firePoint.position + new Vector3(-1.5f, 0, 0), new Quaternion(deltax / dist, deltay / dist, 0, 0));
            }
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * speed, ForceMode2D.Impulse);
        }
        else
        {
            timerEnemy += Time.deltaTime; ;
        }
    }

    public void CreateSniper(float x, float y) //créer un sniper là où la balle se détruit
    {
        if (Random.Range(0,100) < 10)
        {
            GameObject sniper = Instantiate(sniperPrefab, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
        }
        
    }

}
