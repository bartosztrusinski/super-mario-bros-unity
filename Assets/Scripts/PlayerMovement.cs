using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 8f;
    public float maxJumpHeight = 5f; // In units
    public float maxJumpTime = 1f; // In seconds

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime * 0.5f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime * 0.5f, 2f);

    public bool isGrounded { get; private set; }
    public bool isJumping { get; private set; }

    private new Rigidbody2D rigidbody;
    private new Camera camera;
    private Vector2 velocity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    void Update()
    {
        UpdateHorizontalVelocity();

        isGrounded = rigidbody.Raycast(0.25f, 0.375f, Vector2.down);

        if (isGrounded)
        {
            UpdateVerticalVelocity();
        }

        ApplyGravity();
        ApplyTerminalVelocity();
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }

    void OnCollisionEnter2D(Collision2D obstacle)
    {
        if (!IsPowerUp(obstacle.gameObject) && IsBumpingHead(obstacle))
        {
            velocity.y = 0f;
        }
    }

    private void UpdateHorizontalVelocity()
    {
        float inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);
    }

    private void UpdateVerticalVelocity()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        isJumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            isJumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool isFalling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = isFalling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
    }

    private void ApplyTerminalVelocity()
    {
        velocity.y = Mathf.Max(velocity.y, gravity * 0.4f);
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

    private bool IsPowerUp(GameObject gameObject)
    {
        return gameObject.layer == LayerMask.NameToLayer("PowerUp");
    }

    private bool IsBumpingHead(Collision2D obstacle)
    {
        return transform.DotProduct(obstacle.transform, Vector2.up) > 0.3f;
    }
}
