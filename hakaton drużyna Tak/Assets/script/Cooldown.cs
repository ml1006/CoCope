using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cooldown
{
    [SerializeField]
    private float cooldownTime;
    private float _nextFireTime = 0.0f;

    public bool IsInCooldown => Time.time < _nextFireTime;
    public void StartCooldown() => _nextFireTime = Time.time + cooldownTime;
    public void ResetCooldown() => _nextFireTime = Time.time;
}
