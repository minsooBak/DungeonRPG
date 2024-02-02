using UnityEngine;

[CreateAssetMenu(fileName = "DefaultObject", menuName = "Data/Object/DefaultObject", order = 0)]
public class ObjectStat : ScriptableObject
{
    [Header("DefaultObject")]
    public int hp;
    public int maxHP;
}
