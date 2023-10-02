using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chili : IInventoryItem
{

    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public Chili(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }

    public IInventoryItem Clone()
    {
        var clone = new Chili(Info);
        clone.State.Amount = State.Amount;
        return clone;
    }
}
