using System;
using UnityEngine;

namespace Weapons
{
    public class Sword : Weapon
    {
        [SerializeField] private int swordDamage;
        [SerializeField] private float swordKnockBack;
        [SerializeField] private float swordStunTime;
        private void Awake()
        {
            Damage = swordDamage;
            KnockBack = swordKnockBack;
            StunTime = swordStunTime;
        }
    }
}