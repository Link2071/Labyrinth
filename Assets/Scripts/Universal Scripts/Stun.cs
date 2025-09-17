using System.Collections;
using UnityEngine;

namespace Universal_Scripts
{
    public class Stun : MonoBehaviour
    {
        [HideInInspector] public bool isStunned;
        [HideInInspector] public bool isBeingKnockedBack;
        public IEnumerator StunCooldown(float stunTime, float knockBackForce = 0, Rigidbody2D rb = null, Vector2 knockBackDirection = default)
        {
            isStunned = true;
            
            
            if (knockBackForce > 0)
            {
                KnockBack(rb, knockBackDirection, knockBackForce);
            }
            yield return new WaitForSeconds(stunTime);
            isStunned = false;
        }

        private void KnockBack(Rigidbody2D rb, Vector2 knockBackDirection, float knockBackForce)
        {
            rb.linearVelocity = knockBackDirection * knockBackForce;
        }
    }
}