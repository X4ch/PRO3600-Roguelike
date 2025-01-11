using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniperteleporter : EnemyObject
{
    private float timerEnemy = -1;
    public float speed = 20f;
    public Transform firePoint;
    private Quaternion rot;
    public GameObject bulletPrefab;

    //voir le code de Sniper pour le corps principal
    protected override void Move()
    {
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
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * speed, ForceMode2D.Impulse);
        }
        else
        {
            timerEnemy += Time.deltaTime; ;
        }
    }

    public void Teleport(float x, float y) // il se téléporte là où la balle explose
    {
        rgbd.transform.position = new Vector2(x, y);
    }

}
