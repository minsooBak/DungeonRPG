using UnityEngine;

public static class Utils
{
    //���־��� �Լ���
    public static float DirectionToAngle(float x, float y)
    {
        return Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }
}
