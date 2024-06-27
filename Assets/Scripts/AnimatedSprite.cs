using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameRate = 0.17f;

    private SpriteRenderer spriteRenderer;
    private int currentFrameIndex;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        InvokeRepeating(nameof(Animate), frameRate, frameRate);
    }

    void OnDisable()
    {
        CancelInvoke(nameof(Animate));
    }

    private void Animate()
    {
        currentFrameIndex = (currentFrameIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[currentFrameIndex];
    }
}
