using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create New Item Info")]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private int _maxInventoryItems;
    [SerializeField] private Sprite _icon;

    public string Id => _id;
    public string Title => _title;
    public string Description => _description;
    public int MaxItemsInInventory => _maxInventoryItems;
    public Sprite Icon => _icon;
}
