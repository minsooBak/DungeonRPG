using UnityEngine;

public static class Utils
{
    //자주쓰는 함수들
    public static float DirectionToAngle(float x, float y)
    {
        return Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }
}
