using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : IInventoryItem
{
    public Type Type => GetType();

    public bool IsEquipped { get; set; }

    public int MaxItemsInInventorySlot { get; }

    public int Amount { get; set; }

    public Apple (int maxItemsInInventorySlot)
    {
        MaxItemsInInventorySlot = maxItemsInInventorySlot;
    }

    public IInventoryItem Clone()
    {
        return new Apple(MaxItemsInInventorySlot)
        {
            Amount = this.Amount
        };
    }
}
