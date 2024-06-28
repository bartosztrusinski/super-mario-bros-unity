using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flattenedSprite;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPlayer(collision.gameObject) && IsGettingStomped(collision))
        {
            Flatten();
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

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flattenedSprite;
        Destroy(gameObject, 1f);
    }
}
