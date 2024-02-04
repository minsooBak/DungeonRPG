using UnityEngine;

[CreateAssetMenu(fileName = "MovementObject", menuName = "Data/Object/MovementObject", order = 1)]
public class MovementObjectStat : ObjectStat
{ 
    [Space(10)]
    [Header("MovementObject")]
    public float speed;
    public float jumpForce;
    public int mp;
    public int maxMP;

    public int Gold;
}
