using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsPlayer(collider))
        {
            collider.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(3f);
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }

    private bool IsPlayer(Collider2D collider)
    {
        return collider.CompareTag("Player");
    }
}
