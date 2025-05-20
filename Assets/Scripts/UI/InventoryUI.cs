using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private List<Image> _images = new();

    [SerializeField] private Inventory _inventory;

    private void Awake() => Init();

    private void OnEnable()
    {
        _inventory.OnInventoryChanged += LoadImage;
    }

    private void OnDisable()
    {
        _inventory.OnInventoryChanged -= LoadImage;
    }

    private void Init()
    {
        for (int i = 0; i < 7; i++)
        {
            _images.Add(transform.GetChild(i).GetComponent<Image>());
        }
    }

    private void LoadImage()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            if (i >= _inventory.Slots.Count)
            {
                _images[i].sprite = null;
                continue;
            }

            _images[i].sprite = _inventory.Slots[i].Icon;
        }
    }
}
