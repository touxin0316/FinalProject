using UnityEngine;

public static class UtilLib
{

    public static float GetAngleFromDirection(Vector3 direction)
    {
        return Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    public static bool InRange(Vector3 posA, Vector3 posB, float distance)
    {
        float dX = posA.x - posB.x;
        float dY = posA.y - posB.y;
        float dZ = posA.z - posB.z;
        return dX * dX + dY * dY + dZ * dZ < distance * distance;
        // Cause apparently doing square root is very slow
    }

}
