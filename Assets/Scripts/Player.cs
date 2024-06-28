using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallSpriteRenderer, bigSpriteRenderer;

    public bool IsBig => bigSpriteRenderer.enabled;
    public bool IsSmall => smallSpriteRenderer.enabled;
    public bool IsDead => deathAnimation.enabled;

    private DeathAnimation deathAnimation;

    void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public void GetHit()
    {
        if (IsBig)
        {
            Shrink();
        }
        else if (IsSmall)
        {
            Die();
        }

    }

    private void Shrink()
    {
        // TODO
    }

    private void Die()
    {
        smallSpriteRenderer.enabled = false;
        bigSpriteRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
}
