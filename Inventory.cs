using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public int space = 20;

    public List<InventorySlot> items = new List<InventorySlot>();

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        Instance = this;
    }

    public bool Add(InventoryItem item, int quantity = 1)
    {
        if (!item.isStackable || !Contains(item))
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(new InventorySlot(item, quantity));
        }
        else
        {
            foreach (InventorySlot slot in items)
            {
                if (slot.item == item)
                {
                    slot.quantity += quantity;
                    break;
                }
            }
        }

        onInventoryChangedCallback?.Invoke();
        return true;
    }

    public void Remove(InventoryItem item, int quantity = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                items[i].quantity -= quantity;
                if (items[i].quantity <= 0)
                {
                    items.RemoveAt(i);
                }
                onInventoryChangedCallback?.Invoke();
                return;
            }
        }
    }

    public bool Contains(InventoryItem item)
    {
        foreach (InventorySlot slot in items)
        {
            if (slot.item == item) return true;
        }
        return false;
    }
}
