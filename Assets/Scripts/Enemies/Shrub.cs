using System;
using UnityEngine;

namespace Enemies
{
    public class Shrub : Enemy
    {
        [SerializeField] private Transform player;
        
        void Update()
        {
            switch (StunCooldown)
            {
                case > 0:
                    StunCooldown -= Time.deltaTime;
                    return;
                case <= 0:
                    StunCooldown = 0;
                    break;
            }
            
            Move(player);

            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
