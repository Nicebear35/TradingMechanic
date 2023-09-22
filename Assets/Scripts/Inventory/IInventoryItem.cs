using System;

public interface IInventoryItem 
{
    Type Type { get; }
    bool IsEquipped { get; set; }
    int MaxItemsInInventorySlot { get; }
    int Amount { get; set; }

    IInventoryItem Clone(); 
}
