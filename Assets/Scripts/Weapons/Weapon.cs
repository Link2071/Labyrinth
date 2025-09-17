using System;
using Enemies;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        protected int Damage;
        protected float KnockBack;
        protected float StunTime;


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(Damage, KnockBack, StunTime);
            }
        }
    }
}
