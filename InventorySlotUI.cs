using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Text countText;
    private InventorySlot currentSlot;

    public void AddItem(InventorySlot slot)
    {
        currentSlot = slot;
        icon.sprite = slot.item.icon;
        icon.enabled = true;
        countText.text = slot.item.isStackable ? slot.quantity.ToString() : "";
    }

    public void ClearSlot()
    {
        currentSlot = null;
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
    }
}
