using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerV2 : MonoBehaviour
{   
    // declaration des variables utiles

    public float moveSpeed = 8f;

    public Rigidbody2D rb;
    public Animator animator;
    public CapsuleCollider2D boxcollider;

    public HealthBar healthBar;
    public ManaBar manaBar;

    public int health = 100;
    public int currentHealth;

    public int maxMana = 30;
    public int currentMana;

    private float invframe = 0;
    public Vector2[] smell;
    Vector2 movement;
    public float smellTimer;

    public FloorManager floorManager;
    public Inventory inventoryManager;

    void Start()
    {
        healthBar = GameObject.FindGameObjectsWithTag("HealthBar")[0].GetComponent<HealthBar>(); // permet de recuper le gameObject healthBar pour le prefab player
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<ManaBar>();           // permet de recuper le gameObject manaBar pour le prefab player
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
        floorManager = GameObject.FindGameObjectWithTag("FloorManager").GetComponent<FloorManager>();


        currentMana = maxMana;
        currentHealth = health;

        smell = new Vector2[29];
        smellTimer = -1;

        manaBar.SetMaxMana(maxMana);                                                              // initialise les barres de vie et de mana a leur maximum
        healthBar.SetMaxHealth(health);

    }

    // Update is called once per frame
    void Update()
    {
        IsDead();
        manaBar.SetMana(currentMana);       // initialise les barres de mana et de vie en fonction des ressources en temps reel
        healthBar.SetHealth(currentHealth);             

        if (smellTimer < 0) //ce timer va determiner les instant où on enregistre l'emplacement du joueur pour l'ennemi dogot
        {
            smellTimer = 0.5f;
            DefineSmell();
        }

        smellTimer -= Time.deltaTime;

        invframe -= Time.deltaTime;
        movement.x = Input.GetAxisRaw("Horizontal"); // permet le deplacement horizontal et vertical du joueur
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x); // animations associes aux deplacements
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);  

        if (Mathf.Abs(movement.y)+ Mathf.Abs(movement.y) == 2) // Reguler la vitesse sur les directions diagonales
        {
            movement.y *= 1/Mathf.Sqrt(2);
            movement.x *= 1/Mathf.Sqrt(2);
        }

    }
 
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);    //ajuste la vitesse

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        

    }

    // Prend des degats au corps a corps
    public void GetHit(int damage) 
    {
        if (invframe < 0)
        {
            currentHealth -= damage;
            invframe = 0.5f;
        }
    }
    

    // Verifie si le joueur est mort 
    void IsDead()
    {
        // Quand le joueur meurt, la partie recommence du début, son inventaire est réinitialisé et on remet le mana et la vie au maximum
        if (currentHealth <= 0)
        {
            floorManager.currentFloor = 0;
            inventoryManager.inventoryReset();
            floorManager.generateNextFloor();
            currentHealth = health;
            currentMana = maxMana;
        }
    }

    private void DefineSmell()//les éléments du tableau sont remplis de manière à ce que le plus récent est le premier élément non nul 
    {
        if (smell[0] != new Vector2(0, 0)) 
        {
            for (int i = 27; i>=0; i--)
                {
                    smell[i + 1] = smell[i];
                }
            smell[0] = transform.position;
        }
        else
        {
            for (int i = 28; i>=0; i--) //rempli le premier élément non nul en partant de la fin
            {
                
                if (smell[i] == new Vector2(0,0))
                {
                    smell[i] = transform.position;
                }
                    
            }
        }
    }
}

