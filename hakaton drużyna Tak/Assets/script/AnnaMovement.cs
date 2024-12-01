using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// https://github.com/h8man/NavMeshPlus

[RequireComponent(typeof(NavMeshAgent))]
public class AnnaMovement : MonoBehaviour
{
    private Transform target;
    NavMeshAgent agent;
    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>(); 
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void FixedUpdate()
    {
        Vector3 velocity = agent.velocity.normalized;
        if(velocity!=Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        animator.SetFloat("MoveX", velocity.x);
        animator.SetFloat("MoveY", velocity.y);

    }
    public void AssignTarget(Transform destination)
    {

        target = destination;
        agent.SetDestination(target.position);
    }
}



//animator.Setfloat("isMoving", true);