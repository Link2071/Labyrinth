using System;
using UnityEngine;
using Weapons;

public class Spear : Weapon
{
    [SerializeField] private int spearDamage;
    [SerializeField] private float spearKnockBack;
    [SerializeField] private float spearStunTime;


    private void Awake()
    {
        Damage = spearDamage;
        KnockBack = spearKnockBack;
        StunTime = spearStunTime;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(Damage, StunTime);
        }
    }
}
