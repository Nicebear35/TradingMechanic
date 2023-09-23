using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private IInventory _inventory;

    private void Awake()
    {
        int capacity = 10;

        _inventory = new InventoryWithSlots(capacity);

        Debug.Log($"Created new inventory. Capacity: {capacity}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddRandomApples();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveRandomApples();
        }
    }

    private void AddRandomApples()
    {
        var count = Random.Range(1, 5);
        var apple = new Apple(5);
        apple.Amount = count;
        _inventory.TryToAdd(this, apple);
    }

    private void RemoveRandomApples()
    {
        var countToRemove = Random.Range(1, 10);
        _inventory.RemoveItem(this, typeof(Apple), countToRemove);
        Debug.Log($"Будет удалено {countToRemove} яблок");    
    }
}
