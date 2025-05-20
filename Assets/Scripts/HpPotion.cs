using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hp Potion", menuName = "Scriptable Object/Hp Potion", order = 1)]
public class HpPotion : ItemData
{
    public int value;

    public override void Use(PlayerController controller)
    {
        controller.RecoveryHp(value);
    }
}
