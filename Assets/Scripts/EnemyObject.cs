using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyObject : MonoBehaviour
{
    // declaration des variables utiles

    public LayerMask playerlayer;
    public Collider2D boxcollider;
    public Rigidbody2D rgbd;
    public float inverseMoveTime;
    public Transform target;
    private int directdamage = 1;
    public bool detectarget = false;
    public bool is_hittable;
    public GameObject enemy;
    public PlayerV2 player;
    public Animator animator;

    public int maxHealth = 100;
    public int currentHealth;
    public float size;
    public bool isBoss;
    public HealthBar healthBar;

    public GameObject currentRoom;
    public GameObject canvas;
    private float waitstart;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            currentRoom = collision.gameObject;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            if (collision.gameObject == currentRoom)
            {
                currentRoom = null;
            }
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerV2>(); // trouve le joueur spécifiquement pour l'ennemi dogot
        boxcollider = GetComponent<Collider2D>();
        rgbd = GetComponent<Rigidbody2D>();
       
        target = GameObject.FindWithTag("Player").transform;
        is_hittable = true;
        currentHealth = maxHealth; 
        size = boxcollider.bounds.size.sqrMagnitude;
        waitstart = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (waitstart >1 ) //l'ennemi attend 1 seconde avant de démarrer pour laisser au joueur le temps d'analyser la situation
        {
            Move();
            DirectHit();
            CheckAnimation();
            if (isBoss) // active la barre de vie des boss
            {
                healthBar = GameObject.FindGameObjectsWithTag("HealthBar")[1].GetComponent<HealthBar>(); canvas.SetActive(true);
                healthBar.SetHealth(currentHealth);
            }
        
            
        }
        else { waitstart += Time.deltaTime; }
    }


    //fonction qui verifie si un joueur est detecte dans un cercle de rayon 0.8 autour du centre de l'ennemi.
    void DirectHit()
    {
        if (is_hittable) // certains ennemis, berserker immobile et firstboss lorsqu'il saute; ne sont pas sensible mais ne peuvent aussi pas infliger de dégats au joueur
        {
            Collider2D[] hitzone = Physics2D.OverlapCircleAll(rgbd.position,0.8f);
            foreach (Collider2D player in hitzone)
            {
                if (player.tag == "Player")
                {
                    player.GetComponent<PlayerV2>().GetHit(directdamage); //si un joueur est detecte, il prend des degats
                }
            } 
        }
        


    }

    //fonction qui va definir les deplacements, ce qui rend pour l'instant les ennemis uniques
    protected abstract void Move();

    //permet aux ennemis de prendre des degats et de mourir
    public void TakeDamage(int damage)
    {
        if (is_hittable) {
            Debug.Log("Ennemy is hurt");

            currentHealth -= damage;

            //animator.SetTrigger("Hurt");

            if(currentHealth <= 0)
            {
                Die();
            } 
        }
        
        
    //tue les ennemis
    void Die()
        {    
            Debug.Log("Enemy died!"); 
            
            //animator.SetTrigger("IsDead",true);

            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false; 
            rgbd.velocity = new Vector2(0, 0);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            Destroy(enemy);
        }
    }
    //gère l'affectation du bon sprite en fonction de où regarde l'ennemi. Puur les ennemis comme rocketbeast et firstboss, lorsqu'ils ne bougent pas, ils ont en réalité une très faible vitesse pour faire s'appliquer l'animation.
    public void CheckAnimation()
    {
        if (rgbd.velocity.x > 0)
        {
            animator.SetBool("facingright", true);
        }
        else if (rgbd.velocity.x < 0)
        {
            animator.SetBool("facingright", false);
        }
    }
}
