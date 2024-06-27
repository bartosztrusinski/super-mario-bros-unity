using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public Sprite idle;
    public Sprite run;
    public Sprite jump;
    public Sprite slide;

    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void LateUpdate()
    {
        spriteRenderer.sprite =
            playerMovement.isJumping ? jump
            : playerMovement.isTurning ? slide
            : playerMovement.isRunning ? run
            : idle;
    }
}
