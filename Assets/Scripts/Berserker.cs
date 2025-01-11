using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Berserker : EnemyObject
{
    private float speed = 8;
    Vector2 end;
    // Update is called once per frame

    //le berserker n'est pas beaucoup différent de FirstEnemy, sauf qu'il ne bouge pas si il ne detecte rien, sa vision est faible, mais sa vitesse est très grande. De plus, il est insensible lorsqu'il est immobile (si on rajoute des attaques à distance)
    protected override void Move()
    {

        Vector2 playerPos = target.position;
        Vector2 selfPos = this.gameObject.transform.position;
        float deltax = (playerPos.x - selfPos.x);
        float deltay = (playerPos.y - selfPos.y);
        float sumdelta = deltax * deltax + deltay * deltay;
        float dist = Mathf.Sqrt(sumdelta); //calcule la distance de berserker au joueur
        //si il est proche, il redevient touchable
        if (dist < 3)
        {
            animator.SetBool("sleeping", false);
            detectarget = true;
            is_hittable = true;
        }
        else
        {
            animator.SetBool("sleeping", true);
            detectarget = false;
            is_hittable = false;
        }
        //ce if permet d'empecher un bug lorsqu'il est trop proche du joueur
        if (dist > 0.7 && detectarget)
        {
            rgbd.velocity = new Vector2(deltax * speed / dist, deltay * speed / dist);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
        }

        //si rien n'est detecté, il ne bouge pas
        else
        {
            rgbd.velocity = new Vector2(0, 0);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
        }

    }




}
