using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NextFloorTeleporter : MonoBehaviour
{   
    public Animator animator;
    public GameObject floorManager;
    public CircleCollider2D circleCollider2D;
    public SpriteRenderer spriteRenderer;

    // Permet de faire en sorte que l'animation de téléportation ce joue avant que le reste des évenements de transition d'étage ce réalise
    private float timer = -21000000;

    // Permet de gérer la téléportation du jouer lorsque celui-ci rentre en collision avec le téléportteur.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = false;
            animator.SetBool("TP",true);
            if (timer <= -2) { timer = -1;};
            
        }
    }

    
    // Gère le minteur qui permet de s'assurer que l'animation de téléportation soit bien jouée
    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1 ) 
        {   
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = true;
            floorManager.GetComponent<FloorManager>().nextFloor();
            
        }
    }

    public void Start()
    {
        spriteRenderer.enabled = false;
        floorManager = GameObject.FindGameObjectsWithTag("FloorManager")[0];
        circleCollider2D.enabled = false;
    }

    // Permet d'activer le téléporteur, cette fonction est appelé une fois que tout les ennemies dans la salle sont vaincue, par le bias du script roomCoords
    public void setActive()
    {
        circleCollider2D.enabled = true;
        spriteRenderer.enabled = true;
    }
}
