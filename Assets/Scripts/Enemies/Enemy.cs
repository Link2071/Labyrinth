using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public int enemyHealth = 100;
        public float moveSpeed = 2f;
        [SerializeField] protected Rigidbody2D enemyRigidbody;

        protected float StunCooldown;
        
        private Vector2 _direction;
        private bool _isStunned;

        protected void Move(Transform playerTransform)
        {
            if(_isStunned)
                return;
            
            Vector2 movementVector = playerTransform.position - transform.position;
            movementVector.Normalize();
            
            _direction = movementVector;
            
            enemyRigidbody.linearVelocity = movementVector * moveSpeed;
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg, Vector3.forward);
        }

        public void TakeDamage(int damage, float knockBack, float stunTime)
        {
            enemyHealth -= damage;
            StartCoroutine(KnockBack(knockBack, stunTime));
            
            print($"Enemy took {damage}");
        }

        IEnumerator KnockBack(float knockBack, float stunTime)
        {
            _isStunned = true;
            
            enemyRigidbody.linearVelocity = -_direction * knockBack;
            print($"Enemy knocked back with {knockBack} force");
            
            yield return new WaitForSeconds(stunTime);

            _isStunned = false;
        }
    }
}
