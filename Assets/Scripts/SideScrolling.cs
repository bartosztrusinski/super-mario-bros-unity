using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        ScrollCamera();
    }

    private void ScrollCamera()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(player.position.x, cameraPosition.x);
        transform.position = cameraPosition;
    }
}
