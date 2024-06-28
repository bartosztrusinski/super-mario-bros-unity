using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;
    public bool shouldShellDisappear = false;

    private bool isInShell;
    private bool isShellPushed;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isInShell && IsPlayer(collision))
        {
            if (IsGettingStomped(collision))
            {
                EnterShell();
            }
            else
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.GetHit();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isInShell && IsPlayer(collider))
        {
            if (!isShellPushed)
            {
                Vector2 direction = new(transform.position.x - collider.transform.position.x, 0f);
                PushShell(direction);

            }
            else
            {
                Player player = collider.GetComponent<Player>();
                player.GetHit();
            }
        }

        if (!isInShell && IsShell(collider))
        {
            GetHit();
        }
    }

    void OnBecameInvisible()
    {
        if (isShellPushed && shouldShellDisappear)
        {
            Destroy(gameObject);
        }
    }

    private bool IsPlayer(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Player");
    }

    private bool IsPlayer(Collider2D collider)
    {
        return collider.CompareTag("Player");
    }

    private bool IsGettingStomped(Collision2D collision)
    {
        return collision.transform.DotProduct(transform, Vector2.down) > 0.3f;
    }

    private void EnterShell()
    {
        isInShell = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        isShellPushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private bool IsShell(Collider2D collider)
    {
        return collider.gameObject.layer == LayerMask.NameToLayer("Shell");
    }

    private void GetHit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
