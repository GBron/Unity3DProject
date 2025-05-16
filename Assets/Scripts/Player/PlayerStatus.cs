using DesignPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(0, 10)]
    public float WalkSpeed { get; set; }

    [field: SerializeField]
    [field: Range(0, 10)]
    public float RunSpeed { get; set; }

    [field: SerializeField]
    [field: Range(0, 10)]
    public float RotateSpeed { get; set; }

    [field: SerializeField]
    [field: Range(0, 500)]
    public int MaxHp { get; set; }

    public ObseravableProperty<int> CurrentHp { get; private set; } = new();
    public ObseravableProperty<bool> IsAiming { get; private set; } = new();
    public ObseravableProperty<bool> IsMoving { get; private set; } = new();
    public ObseravableProperty<bool> IsAttacking { get; private set; } = new();
    
}
