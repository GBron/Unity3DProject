using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> _slots = new();
    public List<ItemData> Slots
    {
        get => _slots;
        set
        {
            _slots = value;
            
        }
    }

    public UnityAction OnInventoryChanged;

    private PlayerController _controller;
    public bool IsEmpty => _slots.Count == 0;

    private void Awake() => Init();

    private void Init()
    {
        _controller = GetComponent<PlayerController>();
    }

    public void GetItem(ItemData itemData)
    {
        _slots.Add(itemData);
        OnInventoryChanged?.Invoke();
    }

    public void UseItem(int index)
    {
        _slots[0].Use(_controller);
        _slots.RemoveAt(index);
        OnInventoryChanged?.Invoke();
    }
}
