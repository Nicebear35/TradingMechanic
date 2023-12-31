using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItemInfo
{
    string Id { get; }
    string Title { get; }
    string Description { get; }
    int MaxItemsInInventory { get; }
    Sprite Icon { get; }
}
