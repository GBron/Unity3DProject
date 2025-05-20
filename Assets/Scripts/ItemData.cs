using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string Name;
    [TextArea] public string Description;
    public Sprite Icon;
    public GameObject Prefab;

    public abstract void Use(PlayerController controller);
}
