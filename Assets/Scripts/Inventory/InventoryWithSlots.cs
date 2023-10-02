using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryWithSlots : IInventory
{
    public int Capacity { get; set; }

    public bool IsFull => _slots.All(slot => slot.IsFull);

    public event Action<object, IInventoryItem, int> OnInventoryItemAdded;
    public event Action<object, Type, int> OnInventoryItemRemoved;
    public event Action<object> OnInventoryStateChanged;

    private List<IInventorySlot> _slots;


    public InventoryWithSlots(int capacity)
    {
        Capacity = capacity;

        _slots = new List<IInventorySlot>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }

    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.ItemType == itemType).Item;
    }

    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();

        foreach (var slot in _slots)
        {
            if (!slot.IsEmpty)
            {
                allItems.Add(slot.Item);
            }
        }

        return allItems.ToArray();
    }

    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();
        var slotsOfType = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType);

        foreach (var slot in slotsOfType)
        {
            allItemsOfType.Add(slot.Item);
        }

        return allItemsOfType.ToArray();
    }

    public IInventoryItem[] GetEquippedItem()
    {
        var requiredSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.Item.State.IsEquipped);
        var equippedItems = new List<IInventoryItem>();

        foreach (var slot in requiredSlots)
        {
            equippedItems.Add(slot.Item);
        }

        return equippedItems.ToArray();
    }

    public int GetItemAmount(Type itemType)
    {
        var amount = 0;
        var allItemSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType);

        foreach (var itemSlot in allItemSlots)
        {
            amount += itemSlot.Amount;
        }

        return amount;
    }

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.Find(slot => !slot.IsEmpty && slot.ItemType == item.Type && !slot.IsFull);

        if (slotWithSameItemButNotEmpty != null)
        {
            return TryAddToSlot(sender, slotWithSameItemButNotEmpty, item);
        }

        var emptySlot = _slots.Find(slot => slot.IsEmpty);

        if (emptySlot != null)
        {
            return TryAddToSlot(sender, emptySlot, item);
        }

        Debug.Log($"Can't add item ({item.Type}), amount ({item.State.Amount}), because there is no place in inventory.");

        return false;
    }

    public bool TryAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.Amount + item.State.Amount <= item.Info.MaxItemsInInventory;
        var amountToAdd = fits ? item.State.Amount : item.Info.MaxItemsInInventory - slot.Amount;
        var amountLeft = item.State.Amount - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.State.Amount = amountToAdd;

        if (slot.IsEmpty)
        {
            slot.SetItem(clonedItem);
        }
        else
        {
            slot.Item.State.Amount += amountToAdd;
        }

        Debug.Log($"Item added to inventory. Item type:{item.Type}, amount: {amountToAdd}");
        OnInventoryItemAdded?.Invoke(sender, item, amountToAdd);
        OnInventoryStateChanged?.Invoke(sender);

        if (amountLeft <= 0)
        {
            return true;
        }

        item.State.Amount = amountLeft;
        return TryToAdd(sender, item);
    }

    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot == toSlot)
        {
            return;
        }

        if (fromSlot.IsEmpty)
        {
            return;
        }

        if (toSlot.IsFull)
        {
            return;
        }

        if (!toSlot.IsEmpty && fromSlot.Item.Info.Id != toSlot.Item.Info.Id)
        {
            return;
        }

        var slotCapacity = fromSlot.Capacity;
        var fits = fromSlot.Amount + toSlot.Amount <= slotCapacity;
        var amountToAdd = fits ? fromSlot.Amount : slotCapacity - toSlot.Amount;
        var amountLeft = fromSlot.Amount - amountToAdd;

        if (toSlot.IsEmpty)
        {
            toSlot.SetItem(fromSlot.Item);
            fromSlot.Clear();

            OnInventoryStateChanged?.Invoke(sender);
        }

        toSlot.Item.State.Amount += amountToAdd;

        if (fits)
        {
            fromSlot.Clear();
        }
        else
        {
            fromSlot.Item.State.Amount = amountLeft;
        }

        OnInventoryStateChanged?.Invoke(sender);
    }

    public void RemoveItem(object sender, Type itemType, int amount = 1)
    {
        var slotsWithItem = GetAllSlots(itemType);

        if (slotsWithItem.Length == 0)
        {
            return;
        }

        var amountToRemove = amount;

        for (int i = slotsWithItem.Length - 1; i >= 0; i--)
        {
            var slot = slotsWithItem[i];

            if (slot.Amount >= amountToRemove)
            {
                slot.Item.State.Amount -= amountToRemove;

                if (slot.Amount <= 0)
                {
                    slot.Clear();
                }

                OnInventoryItemRemoved?.Invoke(sender, itemType, amountToRemove);
                OnInventoryStateChanged?.Invoke(sender);
                break;
            }

            var amountRemoved = slot.Amount;
            amountToRemove -= slot.Amount;
            slot.Clear();

            OnInventoryItemRemoved?.Invoke(sender, itemType, amountRemoved);
        }
    }

    public bool HasItem(Type type, out IInventoryItem item)
    {
        item = GetItem(type);
        return item != null;
    }

    public IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).ToArray();
    }

    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }
}
