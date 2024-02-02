using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttack", menuName = "Data/Attack/DefaultAttack", order = 0)]
public class AttackData : ScriptableObject
{
    [Header("DefaultAttack")]
    public int power;
    public int defense;
    public float delay;
    public float range;

    public string targetTag;
    //TODO DropItem
}
