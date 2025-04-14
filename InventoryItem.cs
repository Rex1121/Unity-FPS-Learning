using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool isStackable = false;
    public int maxStack = 1;
}
