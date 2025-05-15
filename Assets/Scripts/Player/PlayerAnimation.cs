using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerStatus _playerStatus;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerStatus = GetComponent<PlayerStatus>();
    }

    public void SetAiming()
    {
        bool isAiming = _playerStatus.IsAiming.Value;
        _animator.SetBool("IsAiming", isAiming);
    }

    public void SetMove()
    {
        bool isMoving = _playerStatus.IsMoving.Value;
        _animator.SetBool("IsMoving", isMoving);
    }
}
