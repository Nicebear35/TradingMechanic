using System;

public interface IInventorySlot
{
    public bool IsFull { get; }
    public bool IsEmpty{ get; }
    
    IInventoryItem Item { get; }
    Type ItemType { get; }
    int Amount { get; }
    int Capacity { get; }

    void SetItem(IInventoryItem item);
    void Clear();
}
