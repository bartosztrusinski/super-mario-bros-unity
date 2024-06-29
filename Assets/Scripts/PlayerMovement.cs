using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 8f;
    public float maxJumpHeight = 5f; // In units
    public float maxJumpTime = 1f; // In seconds

    public float JumpForce => (2f * maxJumpHeight) / (maxJumpTime * 0.5f);
    public float Gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime * 0.5f, 2f);

    public bool IsGrounded { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsRunning => Mathf.Abs(velocity.x) > 0.25f || Math.Abs(inputAxis) > 0.25f;
    public bool IsTurning => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    public AudioSource jumpSound;

    private new Rigidbody2D rigidbody;
    private new Camera camera;
    private Vector2 velocity;
    private float inputAxis;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    void Update()
    {
        UpdateHorizontalVelocity();

        IsGrounded = IsTouchingGround();

        if (IsGrounded)
        {
            UpdateVerticalVelocity();
        }

        UpdateDirectionRotation();
        ApplyGravity();
        ApplyTerminalVelocity();
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }

    void OnCollisionEnter2D(Collision2D obstacle)
    {
        if (IsEnemy(obstacle.gameObject) && IsStomping(obstacle))
        {
            velocity.y = JumpForce * 0.5f;
            IsJumping = true;
        }
        else if (!IsPowerUp(obstacle.gameObject) && IsBumpingHead(obstacle))
        {
            velocity.y = 0f;
        }
    }

    private void UpdateHorizontalVelocity()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);

        if (IsRunningIntoObstacle())
        {
            velocity.x = 0f;
        }
    }

    private bool IsRunningIntoObstacle()
    {
        return rigidbody.Raycast(Vector2.right * velocity.x);
    }

    private bool IsTouchingGround()
    {
        return rigidbody.Raycast(Vector2.down);
    }

    private void UpdateVerticalVelocity()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        IsJumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            jumpSound.Play();
            velocity.y = JumpForce;
            IsJumping = true;
        }
    }

    private void UpdateDirectionRotation()
    {
        if (velocity.x == 0f)
        {
            return;
        }

        float playerDirection = velocity.x < 0f ? -1f : 1f;
        Vector3 nextLocalScale = transform.localScale;
        nextLocalScale.x = playerDirection;

        transform.localScale = nextLocalScale;
    }

    private void ApplyGravity()
    {
        bool isFalling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = isFalling ? 2f : 1f;

        velocity.y += Gravity * multiplier * Time.deltaTime;
    }

    private void ApplyTerminalVelocity()
    {
        velocity.y = Mathf.Max(velocity.y, Gravity * 0.6f);
    }

    private void UpdatePosition()
    {
        float cameraLeftEdge = camera.ViewportToWorldPoint(Vector3.zero).x;
        float cameraRightEdge = camera.ViewportToWorldPoint(Vector3.right).x;
        float playerHalfWidth = GetComponent<CapsuleCollider2D>().bounds.extents.x;

        Vector2 nextPosition = rigidbody.position + (velocity * Time.fixedDeltaTime);
        nextPosition.x = Mathf.Clamp(nextPosition.x, cameraLeftEdge + playerHalfWidth, cameraRightEdge - playerHalfWidth);

        rigidbody.MovePosition(nextPosition);
    }

    private bool IsEnemy(GameObject gameObject)
    {
        return gameObject.layer == LayerMask.NameToLayer("Enemy");
    }

    private bool IsStomping(Collision2D obstacle)
    {
        return transform.DotProduct(obstacle.transform, Vector2.down) > 0.3f;
    }

    private bool IsPowerUp(GameObject gameObject)
    {
        return gameObject.layer == LayerMask.NameToLayer("PowerUp");
    }

    private bool IsBumpingHead(Collision2D obstacle)
    {
        return transform.DotProduct(obstacle.transform, Vector2.up) > 0.3f;
    }
}
