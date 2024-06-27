using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    void LateUpdate()
    {
        run.enabled = playerMovement.isRunning;

        spriteRenderer.sprite =
            playerMovement.isJumping ? jump
            : playerMovement.isTurning ? slide
            : !playerMovement.isRunning ? idle : spriteRenderer.sprite;
    }
}
