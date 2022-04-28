using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        consumable,
        important,
    }
    public string itemName;
    public Sprite icon;
    public ItemType type;
    public int heldNum;
    [TextArea]
    public string itemInfo;
}
