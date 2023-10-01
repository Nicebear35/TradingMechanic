using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : IInventoryItem
{

    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public Apple(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clone = new Apple(Info);
        clone.State.Amount = State.Amount;
        return clone;
    }
}
