using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1 : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    public void Updat()
    {
        
           animator.SetBool("fallen", true);
           

    }
}
