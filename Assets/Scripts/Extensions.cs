using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rigidbody, float radius, float distance, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        RaycastHit2D raycastHit = Physics2D.CircleCast(rigidbody.position, radius, direction, distance, layerMask);

        return raycastHit.collider != null && raycastHit.rigidbody != rigidbody;
    }
}
