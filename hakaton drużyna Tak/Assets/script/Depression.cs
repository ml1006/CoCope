using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depression : MonoBehaviour
{
    [SerializeField]
    private Cooldown cooldown;
    [SerializeField]
    private Cooldown reduce;
    [SerializeField]
    private float grow = 0.1f;
    [SerializeField]
    private float shrink = 0.1f;

    [SerializeField]
    private GameObject kart;

    private void Update()
    {
        if (reduce.IsInCooldown)
        {
            float dist = GameManager.instance.distance - shrink * Time.deltaTime;
            dist = dist < 0.0f ? 0.0f : dist;
            GameManager.instance.distance = dist;
        }
        GameManager.instance.distance += grow * Time.deltaTime;
    }

    public void Reduce()
    {
        if (!cooldown.IsInCooldown)
        {
            cooldown.StartCooldown();
            reduce.StartCooldown();
        }
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.instance.activated) {
            Reduce();
            kart.transform.position = new Vector3(10000.0f, 10000.0f, 10000.0f);
        }
    }
}
