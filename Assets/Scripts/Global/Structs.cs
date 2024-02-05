using EnumTypes;
using UnityEngine;

namespace Structs
{
    [System.Serializable]
    public struct ItemBase
    {
        public string Name;
        public string Description;
        public ItemType Type;
        public Sprite icon;

        public GameObject DropItem;
    }
}