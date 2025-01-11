using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NextFloorTeleporter : MonoBehaviour
{   
    public Animator animator;
    public GameObject floorManager;
    public CircleCollider2D circleCollider2D;
    public SpriteRenderer spriteRenderer;

    // Permet de faire en sorte que l'animation de t�l�portation ce joue avant que le reste des �venements de transition d'�tage ce r�alise
    private float timer = -21000000;

    // Permet de g�rer la t�l�portation du jouer lorsque celui-ci rentre en collision avec le t�l�portteur.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = false;
            animator.SetBool("TP",true);
            if (timer <= -2) { timer = -1;};
            
        }
    }

    
    // G�re le minteur qui permet de s'assurer que l'animation de t�l�portation soit bien jou�e
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

    // Permet d'activer le t�l�porteur, cette fonction est appel� une fois que tout les ennemies dans la salle sont vaincue, par le bias du script roomCoords
    public void setActive()
    {
        circleCollider2D.enabled = true;
        spriteRenderer.enabled = true;
    }
}
