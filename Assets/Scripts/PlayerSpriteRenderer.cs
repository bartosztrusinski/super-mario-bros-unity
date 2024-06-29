using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    public SpriteRenderer spriteRenderer { get; private set; }
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
        run.enabled = false;
    }

    void LateUpdate()
    {
        run.enabled = playerMovement.IsRunning;

        spriteRenderer.sprite =
            playerMovement.IsJumping ? jump
            : playerMovement.IsTurning ? slide
            : !playerMovement.IsRunning ? idle : spriteRenderer.sprite;
    }
}
