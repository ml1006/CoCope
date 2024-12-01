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
    [SerializeField]
    private Animation1 obrazek_anim;
    private float timer = 0f;
    private float timeLimit = 5f;
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
    }

    void Inputs()
    {
        // Movement
        float xMove=0;
        float yMove=0;
            if(Input.GetKey(KeyCode.H))
            {
                xMove=-1;
            }
            if(Input.GetKey(KeyCode.K))
            {
                xMove=1;
            }
            if(Input.GetKey(KeyCode.U))
            {
                yMove=1;
            }
            if(Input.GetKey(KeyCode.J))
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
        bool booo = Input.GetKey(KeyCode.I);



        if(colider.CollidesWithInteraction())
        {
            //Debug.Log("1Interaction!");
            if (booo&&colider.ColliderId() == 0)
            {
                animator.SetBool("boo", booo);
                //GameObject targetObject = GameObject.Find("Square");
                //targetObject.transform.position = new Vector3(-2, 20, 0);
                //targetObject.transform.GetComponent<Rigidbody2D>().MovePosition(targetObject.transform.position + new Vector3(3, 2, 0));
                

                obrazek_anim.Updat();

                //Debug.Log("Interaction!");

                BoxCollider2D boxCollider0 = BoxColliders[1].transform.GetComponent<BoxCollider2D>();
                boxCollider0.enabled = false;

                
                BoxCollider2D boxCollider = BoxColliders[0].transform.GetComponent<BoxCollider2D>();
                boxCollider.enabled = true;

                GameManager.instance.activated = true;
            }
            
            // Logic for interaction
        }
        else
        {
            animator.SetBool("boo", booo);
        }
    }
         private void OnCollisionEnter2D(Collision2D other)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),other.collider);
        }

}
