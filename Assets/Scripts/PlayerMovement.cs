using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 8f;

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
        UpdateVelocity();
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdateVelocity()
    {
        float inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);
    }

    private void UpdatePosition()
    {
        Vector2 nextPosition = rigidbody.position + (velocity * Time.fixedDeltaTime);
        float cameraLeftEdge = camera.ViewportToWorldPoint(Vector3.zero).x;
        float cameraRightEdge = camera.ViewportToWorldPoint(Vector3.right).x;
        float playerHalfWidth = GetComponent<CapsuleCollider2D>().bounds.extents.x;

        nextPosition.x = Mathf.Clamp(nextPosition.x, cameraLeftEdge + playerHalfWidth, cameraRightEdge - playerHalfWidth);

        rigidbody.MovePosition(nextPosition);
    }
}
