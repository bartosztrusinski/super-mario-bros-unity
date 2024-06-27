using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 8f;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        rigidbody.MovePosition(nextPosition);
    }
}
