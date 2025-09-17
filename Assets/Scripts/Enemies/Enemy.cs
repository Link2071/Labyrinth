using System;
using System.Collections;
using UnityEngine;
using Universal_Scripts;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public int enemyHealth = 100;
        public float moveSpeed = 2f;

        [Header("Components")] 
        [SerializeField] protected Rigidbody2D enemyRigidbody;
        [SerializeField] protected Stun stunScript;

        protected float AttackCooldown;
        
        private Vector2 _direction;
        private bool _isStunned;
        protected bool IsAttacking;
        protected bool PlayerInRange;

        protected void Move(Vector3 targetPosition)
        {
            if (stunScript.isStunned)
            {
                if (stunScript.isBeingKnockedBack)
                    enemyRigidbody.linearVelocity = Vector2.zero;
                return;
            }
            Vector2 movementVector = targetPosition - transform.position;
            movementVector.Normalize();
            
            _direction = movementVector;
            
            enemyRigidbody.linearVelocity = movementVector * moveSpeed;
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg, Vector3.forward);
        }

        public void TakeDamage(int damage, float knockBack, float stunTime)
        {
            enemyHealth -= damage;
            StartCoroutine(stunScript.StunCooldown(stunTime, knockBack, enemyRigidbody, -_direction));
        }

        protected IEnumerator CheckRange(Transform playerTransform, float range)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) >= range)
            {
                PlayerInRange = false;
                yield break;
            }
            PlayerInRange = true;
        }

        public virtual IEnumerator Attack()
        {
            yield break;
        }
    }
}
