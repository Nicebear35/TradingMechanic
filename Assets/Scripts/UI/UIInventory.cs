using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo _chiliInfo;
    [SerializeField] private InventoryItemInfo _berriesInfo;

    public InventoryWithSlots Inventory => _tester.Inventory;

    private UIInventoryTester _tester;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        _tester = new UIInventoryTester(_chiliInfo,_berriesInfo,uiSlots);
        _tester.FillSlots();
    }
}
