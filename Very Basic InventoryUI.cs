using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryPanel;
    public InventorySlotUI[] slots;

    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.Instance;
        inventory.onInventoryChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
