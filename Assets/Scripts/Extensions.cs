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

    public static float DotProduct(this Transform transformA, Transform transformB, Vector2 directionB)
    {
        Vector2 directionA = transformB.position - transformA.position;
        return Vector2.Dot(directionA.normalized, directionB);
    }
}
