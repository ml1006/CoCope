using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerCollison))]
public class PlyerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;


    private PlayerMotor motor;
    private PlayerCollison colider;

    [SerializeField]
    Transform anna;

    [SerializeField]
    private Cooldown cooldown;
    
    private Animator animator;
    private float timer = 0f;
    private float timeLimit = 4f;
    private bool timerActive = false; 
    private bool sit = false; 
    private bool sountawrrr=true;

    public AudioSource src;
    public AudioClip sfx1;

    public AudioClip sfx2;


    [SerializeField]
    private Scoreboard scoreboard;

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
    public void wrrsfx()
    {
        src.clip = sfx1;
        src.Play();
    }
    public void meeow()
    {
        src.clip = sfx2;
        src.Play();
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
        bool meow = Input.GetKey(KeyCode.Q);
        bool wrr = Input.GetKey(KeyCode.E);

        if (interact && colider.CollidesWithInteraction())
        {
            Debug.Log("Interaction!");
            // Logic for interction
        }
        if (wrr)
        {
            animator.SetBool("wrr", wrr);
            if(sountawrrr)
            {
                wrrsfx();
                sountawrrr=false;
            }
            
            motor.Move(new Vector2(0,0));
        }
        else
        {
            sountawrrr=true;
            animator.SetBool("wrr", wrr);
        }

        if (meow)
        {
            if (!cooldown.IsInCooldown)
            {
                anna.GetComponent<AnnaMovement>().AssignTarget(transform);
                meeow();
                cooldown.StartCooldown();
                scoreboard.AddKot(100);

            }
        }
        if(meow||interact||wrr||xMove!=0||yMove!=0)
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
