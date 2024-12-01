using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerCollison))]
public class PlyerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    public GameObject[] BoxColliders1;

    private PlayerMotor motor;
    private PlayerCollison colider;

    [SerializeField]
    Transform anna;

    [SerializeField]
    private Cooldown cooldown;
    
    private Animator animator;
    private float timer = 0f;
    private float timeLimit = 3f;
    private bool timerActive = false; 
    private bool sit = false; 

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

    void Update()
    {
        Inputs();

        if (timerActive)
        {
            timer += Time.deltaTime;
            if (timer >= timeLimit)
            {
                timerActive = false;
                sit=true;
            }
        }
    }

    void Inputs()
    {
        // Movement
        float xMove=0;
        float yMove=0;
            if(Input.GetKey(KeyCode.A))
            {
                xMove=-1;
            }
            if(Input.GetKey(KeyCode.D))
            {
                xMove=1;
            }
            if(Input.GetKey(KeyCode.W))
            {
                yMove=1;
            }
            if(Input.GetKey(KeyCode.S))
            {
                yMove=-1;
            }

        Vector2 vel = new Vector2(xMove, yMove);
        vel = vel.normalized * speed;
        
        if(xMove!=0||yMove!=0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

       motor.Move(vel);
        
         
        if(xMove>0)
        {
            gameObject.transform.localScale = new Vector3(1,1,1);
        }   
        if(xMove<0)
        {
            gameObject.transform.localScale = new Vector3(-1,1,1);
        }  

        
        // Activation
        bool interact = Input.GetMouseButtonDown(0);
        bool meow = Input.GetMouseButtonDown(1);


        if (interact && colider.CollidesWithInteraction())
        {
            //Debug.Log("Interaction!");
            if (colider.ColliderId() == 1)
            {
                GameObject targetObject = GameObject.Find("Square");
                targetObject.SetActive(false);

                BoxCollider2D boxCollider0 = BoxColliders1[0].transform.GetComponent<BoxCollider2D>();
                boxCollider0.enabled = false;


            }
            // Logic for interction
        }

        if (meow)
        {
            if (!cooldown.IsInCooldown)
            {
                Debug.Log("MEOW!");
                anna.GetComponent<AnnaMovement>().AssignTarget(transform);

                cooldown.StartCooldown();
            } else
            {
                Debug.Log("MEOWING in cooldown!");
            }
        }
        if(meow||interact||xMove!=0||yMove!=0)
        {
            sit=false;
            timerActive = false;
        }
        else
        {
            if(!timerActive) timer = 0f;
            timerActive = true; 
 
        }
        animator.SetBool("sit", sit);
    }
}
