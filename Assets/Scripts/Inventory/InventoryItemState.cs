using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class InventoryItemState : IInventoryItemState
{
    private int _itemAmount;
    private bool _isItemEquipped;

    public int Amount { get => _itemAmount; set => _itemAmount = value; }
    public bool IsEquipped { get => _isItemEquipped; set => _isItemEquipped = value; }

    public InventoryItemState()
    {
        _itemAmount = 0;
        _isItemEquipped = false;
    }
}
