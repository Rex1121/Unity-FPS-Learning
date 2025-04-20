[System.Serializable]
public class InventorySlot
{
    public InventoryItem item;
    public int quantity;

    public InventorySlot(InventoryItem newItem, int newQuantity)
    {
        item = newItem;
        quantity = newQuantity;
    }
}
