using System;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }

    void OnEnable()
    {
        rigidbody.WakeUp();
    }

    void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    void FixedUpdate()
    {
        UpdatePosition();

        if (IsRunningIntoObstacle())
        {
            direction = -direction;
        }

        if (IsGrounded())
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }

    private void UpdatePosition()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }

    private bool IsRunningIntoObstacle()
    {
        return rigidbody.Raycast(direction);
    }

    private bool IsGrounded()
    {
        return rigidbody.Raycast(Vector2.down);
    }
}
