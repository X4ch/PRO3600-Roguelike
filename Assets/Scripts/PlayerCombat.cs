using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //declaration des variables utiles aux script
    public Animator animator;

    public Transform Attackpoint;
    public float AttackRange = 0.3f;
    public LayerMask ennemyLayers;

    public int attackDamage = 35;
    
    public float Attackrate = 2f; 
    float nextAttackTime = 0f;
   
    // Update is called once per frame
    void Update()                       // Le joueur attaque a chaque fois que l'on appuie sur A
    {
        if(Time.time >= nextAttackTime ) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1 / Attackrate;        // Permet d'avoir une frequence d'attaque
            }   

        }
        
    }

    void Attack()     
    {
        animator.SetTrigger("Attack");

        Debug.Log("Attacking");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Attackpoint.position, AttackRange, ennemyLayers); // On range dans un tableau toutes les collider dans un cercle de rayon AttackRange et portant le layer

        foreach(Collider2D enemy in hitEnemies)                                                                // Chaque ennemis dans le tableau prenent des degats
        {
            enemy.GetComponent<EnemyObject>().TakeDamage(attackDamage);
        }


    }

    void OnDrawGizmosSelected()                                                                               // Permet de voir la hitbox de l'attaque de notre player sur la scene
    {   
        if(Attackpoint == null)
            return;
        Gizmos.DrawWireSphere(Attackpoint.position, AttackRange);
    }

}
