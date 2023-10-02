using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _amountText;

    public IInventoryItem Item { get; private set; }

    public void Refresh(IInventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            CleanUp();
            return;
        }

        Item = slot.Item;
        _iconImage.sprite = Item.Info.Icon;
        _iconImage.gameObject.SetActive(true);

        bool isTextAmountEnabled = slot.Amount > 1;
        _amountText.gameObject.SetActive(isTextAmountEnabled);

        if (isTextAmountEnabled)
        {
            _amountText.text = $"x{slot.Amount.ToString()}";
        }
    }

    private void CleanUp()
    {
        _iconImage.gameObject.SetActive(false);
        _amountText.gameObject.SetActive(false);
    }
}
