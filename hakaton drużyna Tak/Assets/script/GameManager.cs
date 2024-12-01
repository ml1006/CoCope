using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float distance = 5.0f;
    public bool activated = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
}
