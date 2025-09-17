using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Universal_Scripts;

public enum Direction
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3
}

public class PlayerController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference attackAction;
    
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float health = 100f;
    
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Stun stunScript;
    
    [Header("References")]
    [SerializeField] private GameObject swordObject;

    private int _direction;

    private void OnEnable()
    {
        moveAction.action.Enable();
        attackAction.action.Enable();

        attackAction.action.performed += Attack;
    }
    
    void FixedUpdate()
    {
        Vector2 movementVector = (moveAction.action.ReadValue<Vector2>().normalized) * moveSpeed;
        rb.linearVelocity = movementVector;
        
        if(moveAction.action.ReadValue<Vector2>() != Vector2.zero)
            _direction = (int)GetDirection(moveAction.action.ReadValue<Vector2>());
    }

    private Direction GetDirection(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.y))
        {
            return movementVector.x > 0 ? Direction.Right : Direction.Left;
        }
        else
        {
            return movementVector.y > 0 ? Direction.Up : Direction.Down;
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        int attackPosition = 0;
        
        switch (_direction)
        {
            case 0:
                attackPosition = 0;
                break;
            
            case 1:
                attackPosition = 90;
                break;
            
            case 2:
                attackPosition = 180;
                break;
            
            case 3:
                attackPosition = 270;
                break;
        }
        
        Vector3 rotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, attackPosition);
        Instantiate(swordObject, transform.position, Quaternion.Euler(rotation), transform);
    }

    public void TakeDamage(int damage, float stunTime)
    {
        health -= damage;
        stunScript.StunCooldown(stunTime);
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        attackAction.action.Disable();
    }
}
