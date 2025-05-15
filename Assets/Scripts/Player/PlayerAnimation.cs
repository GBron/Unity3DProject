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

    public void SetAiming(bool value)
    {
        _animator.SetBool("IsAiming", value);
    }

    public void SetMove(bool value)
    {
        _animator.SetBool("IsMoving", value);
    }

    public void SetAttack(bool value)
    {
        _animator.SetBool("IsAttacking", value);
    }

    public void SetX(float value)
    {
        _animator.SetFloat("X", value);
    }

    public void SetZ(float value)
    {
        _animator.SetFloat("Z", value);
    }
}
