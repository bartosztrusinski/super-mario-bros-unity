using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flattenedSprite;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPlayer(collision.gameObject))
        {
            if (IsGettingStomped(collision))
            {
                Flatten();
            }
            else
            {
                Player player = collision.gameObject.GetComponent<Player>();

                if (player.starpower)
                {
                    Flatten();
                }
                else {

                    player.GetHit();


                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsShell(collider))
        {
            GetHit();
        }
    }

    private bool IsPlayer(GameObject gameObject)
    {
        return gameObject.CompareTag("Player");
    }

    private bool IsGettingStomped(Collision2D collision)
    {
        return collision.transform.DotProduct(transform, Vector2.down) > 0.3f;
    }

    private bool IsShell(Collider2D collider)
    {
        return collider.gameObject.layer == LayerMask.NameToLayer("Shell");
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flattenedSprite;
        Destroy(gameObject, 0.75f);
    }

    private void GetHit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
