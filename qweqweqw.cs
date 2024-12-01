using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerCollison))]
public class PlyerController2 : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    public GameObject[] BoxColliders;

    private PlayerMotor motor;
    private PlayerCollison colider;

    [SerializeField]
    Transform anna;

    [SerializeField]
    private Cooldown cooldown;
    
    private Animator animator;
    private float timer = 0f;
    private float timeLimit = 5f;
    private bool timerActive = false; 
    private bool sit = false;

    [SerializeField]
    private Animation1 obrazek_anim;

    private void Awake() 
    {
        //animator = GetComponent<Animator>(); 
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
   

    }

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        colider = GetComponent<PlayerCollison>();
        

    }



   
 
        
        // Activation
        bool interact = Input.GetMouseButtonDown(0);


        // Debug.Log(colider.CollidesWithInteraction());
        if (interact && colider.CollidesWithInteraction())
        {
            //Debug.Log("1Interaction!");
            if (colider.ColliderId() == 0)
            {
                GameObject targetObject = GameObject.Find("Square");
                //targetObject.transform.position = new Vector3(-2, 20, 0);
                //targetObject.transform.GetComponent<Rigidbody2D>().MovePosition(targetObject.transform.position + new Vector3(3, 2, 0));
                

                obrazek_anim.Updat();

                //Debug.Log("Interaction!");

                BoxCollider2D boxCollider0 = BoxColliders[1].transform.GetComponent<BoxCollider2D>();
                boxCollider0.enabled = false;

                
                BoxCollider2D boxCollider = BoxColliders[0].transform.GetComponent<BoxCollider2D>();
                boxCollider.enabled = true;
            }
            
            // Logic for interaction
        }

        
