using System;

public interface IInventory
{
    int Capacity { get; set; }
    bool IsFull { get; }

    IInventoryItem GetItem(Type itemType);
    IInventoryItem[] GetAllItems();
    IInventoryItem[] GetAllItems(Type itemType);
    IInventoryItem[] GetEquippedItem();

    int GetItemAmount(Type itemType);

    bool AddItem(object sender, IInventoryItem item);
}
