using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // public : on y accede de partout
    private float speed = 10f;
    public new Rigidbody2D rigidbody;
    public CapsuleCollider2D boxcollider;
    public int health=100;
    private float invframe = 0;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<CapsuleCollider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        IsDead();
        invframe -= Time.deltaTime;
        float horizontalInput = 0f;
        float verticalInput = 0f;
        
        // Les touches directionnelles ZQSD déclenchent les animations de déplacement
        if (Input.GetKey(KeyCode.D))    
        {
            horizontalInput += 1;
            animator.SetTrigger ("right");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            horizontalInput -= 1;
            animator.SetTrigger ("left");
        }
        if (Input.GetKey(KeyCode.Z))
        {
            verticalInput += 1;
            animator.SetTrigger ("forward");
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput -= 1;
            animator.SetTrigger ("backward");
        }
        if (Mathf.Abs(verticalInput)+ Mathf.Abs(horizontalInput) == 2) // Réguler la vitesse sur les directions diagonales
        {
            verticalInput *= 1/Mathf.Sqrt(2);
            horizontalInput *= 1/Mathf.Sqrt(2);
        }
        rigidbody.velocity = new Vector2(horizontalInput * speed, verticalInput * speed); 
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
    }

    // Prend des dégats au corps à corps
    public void GetHit(int damage) 
    {
        if (invframe < 0){ 
            health -= damage;
            invframe = 0.5f;

        }
    }
    
    // Vérifie si le joueur est mort 
    void IsDead()
    {
        if (health <= 0)
        {
            print("i am deeeeeeeeead");
        }
    }
    /*
    public static Vector2 GetPosition(Rigidbody2D rigidbody)
    {
        return rigidbody.position;
    }*/
}
