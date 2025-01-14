using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallSpriteRenderer, bigSpriteRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public bool IsBig => bigSpriteRenderer.enabled;
    public bool IsSmall => smallSpriteRenderer.enabled;
    public bool IsDead => deathAnimation.enabled;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;
    public bool starpower { get; private set; }


    void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallSpriteRenderer;
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

    private void Die()
    {
        smallSpriteRenderer.enabled = false;
        bigSpriteRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }

    public void Grow()
    {
        smallSpriteRenderer.enabled = false;
        bigSpriteRenderer.enabled = true;
        activeRenderer = bigSpriteRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    public void Shrink()
    {
        smallSpriteRenderer.enabled = true;
        bigSpriteRenderer.enabled = false;
        activeRenderer = smallSpriteRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallSpriteRenderer.enabled = !smallSpriteRenderer.enabled;
                bigSpriteRenderer.enabled = !smallSpriteRenderer.enabled;
            }

            yield return null;
        }

        smallSpriteRenderer.enabled = false;
        bigSpriteRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
    }
}
