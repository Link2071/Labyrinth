using System;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
}

public class PlayerController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference attackAction;
    
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 5f;
    
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("References")]
    [SerializeField] private GameObject swordObject;

    private int _direction;

    private void OnEnable()
    {
        moveAction.action.Enable();
        attackAction.action.Enable();

        attackAction.action.performed += Attack;
    }
    
    void Update()
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
            return movementVector.x > 0 ? Direction.right : Direction.left;
        }
        else
        {
            return movementVector.y > 0 ? Direction.up : Direction.down;
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
        
        Vector3 Rotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, attackPosition);
        Instantiate(swordObject, transform.position, Quaternion.Euler(Rotation), transform);
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        attackAction.action.Disable();
    }
}
