using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IInventoryItemState
{
    int Amount { get; set; }
    bool IsEquipped { get; set; }
}
