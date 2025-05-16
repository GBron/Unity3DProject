using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _aimAnimator;
    private Animator _animator;
    private PlayerStatus _playerStatus;
    private Image _aimImage;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerStatus = GetComponent<PlayerStatus>();
        _aimImage = _aimAnimator.GetComponent<Image>();
    }

    public void SetAiming(bool value)
    {
        if (!_aimImage.enabled)
        {
            _aimImage.enabled = true;
        }
        _animator.SetBool("IsAiming", value);
        _aimAnimator.SetBool("IsAiming", value);
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
