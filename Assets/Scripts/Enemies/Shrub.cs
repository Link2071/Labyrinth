using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Shrub : Enemy
    {
        [SerializeField] private Transform player;
        
        [Header("Stats")]
        [SerializeField] private int shrubHealth;
        [SerializeField] private float shrubMoveSpeed;
        [SerializeField] private float shrubAttackCooldown;
        [SerializeField] private float shrubAttackDamage;
        [SerializeField] private float shrubAttackRange;
        
        [Header("Enemy Weapons")]
        [SerializeField] private GameObject shrubStabPrefab;

        
        private Vector3 _targetDirection;
        
        private void Awake()
        {
            enemyHealth = shrubHealth;
            moveSpeed = shrubMoveSpeed;
            AttackCooldown = shrubAttackCooldown;
        }

        void FixedUpdate()
        {
            if (enemyHealth <= 0)
                Destroy(gameObject);
            
            if (!IsAttacking)
                StartCoroutine(CheckRange(player, shrubAttackRange));

            if (PlayerInRange)
            {
                MoveToAttackPosition(player);
                return;
            }

            print($"{PlayerInRange}");
            Move(player.position);
        }

        private void MoveToAttackPosition(Transform playerPosition)
        {
            Vector2 playerRelativePosition = playerPosition.position - transform.position;

            
            if (Mathf.Abs(playerRelativePosition.x) > Mathf.Abs(playerRelativePosition.y))
            {
                _targetDirection = playerRelativePosition.x > 0 ? new Vector3(shrubAttackRange, 0, 0) : new Vector3(-shrubAttackRange, 0,0);
            }
            else
            {
                _targetDirection = playerRelativePosition.y > 0 ? new Vector3(0, shrubAttackRange, 0) : new Vector3(0, -shrubAttackRange,0);
            }
            Move(transform.position + _targetDirection);

            if (Vector2.Distance(_targetDirection, playerPosition.position) < 0.2)
            {
                StartCoroutine(Attack());
            }
        }

        public override IEnumerator Attack()
        {
            IsAttacking = true;
            
            StartCoroutine(stunScript.StunCooldown(AttackCooldown));
            Instantiate(shrubStabPrefab, transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(AttackCooldown);
            IsAttacking = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, shrubAttackRange);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + _targetDirection);
        }
    }
}
