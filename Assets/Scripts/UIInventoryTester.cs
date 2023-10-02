using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIInventoryTester
{
    private IInventoryItemInfo _chiliInfo;
    private IInventoryItemInfo _berriesInfo;
    private UIInventorySlot[] _uiSlots;

    public InventoryWithSlots Inventory { get; }

    public UIInventoryTester(InventoryItemInfo chiliInfo, InventoryItemInfo berriesInfo, UIInventorySlot[] uiSlots)
    {
        _chiliInfo = chiliInfo;
        _berriesInfo = berriesInfo;
        _uiSlots = uiSlots;

        Inventory = new InventoryWithSlots(15);
        Inventory.OnInventoryStateChanged += OnInventoryStateChange;
    }

    public void FillSlots()
    {
        var allSlots = Inventory.GetAllSlots();
        var avaliableSlots = new List<IInventorySlot>(allSlots);

        var filledSlots = 5;

        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomChiliToRandomSlot(avaliableSlots);
            avaliableSlots.Remove(filledSlot);

            filledSlot = AddRandomBerriesToRandomSlot(avaliableSlots);
            avaliableSlots.Remove(filledSlot);

        }

        SetupInventoryUI(Inventory);
    }

    private IInventorySlot AddRandomChiliToRandomSlot(List<IInventorySlot> slots)
    {
        var randomSlotIndex = Random.Range(0, slots.Count);
        var randomSlot = slots[randomSlotIndex];
        var randomCount = Random.Range(1, 4);
        var chili = new Chili(_chiliInfo);
        chili.State.Amount = randomCount;

        Inventory.TryAddToSlot(this,randomSlot,chili);
        return randomSlot;
    }

    private IInventorySlot AddRandomBerriesToRandomSlot(List<IInventorySlot> slots)
    {
        var randomSlotIndex = Random.Range(0, slots.Count);
        var randomSlot = slots[randomSlotIndex];
        var randomCount = Random.Range(1, 4);
        var berries = new Berries(_berriesInfo);
        berries.State.Amount = randomCount;

        Inventory.TryAddToSlot(this, randomSlot, berries);
        return randomSlot;
    }

    private void SetupInventoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;

        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    private void OnInventoryStateChange(object sender)
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }
}
