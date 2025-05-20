using DesignPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalMonster : Monster, IDamagable
{
    private bool _isActivateControl = true;
    private bool _canTracking = true;

    [SerializeField] private int MaxHp;
    private ObseravableProperty<int> CurrentHp = new();
    private ObseravableProperty<bool> IsMoving = new();
    private ObseravableProperty<bool> IsAttacking = new();
    private Animator _animator;
    private Collider[] _searchTargets = new Collider[5];
    private Collider[] _traceTarget = new Collider[5];
    private int _layerMask = 1 << 3;
    private Coroutine _atkCoroutine;
    private bool _canAttack => _atkCoroutine == null;

    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private HpGaugeUI _hpGaugeUI;

    private void Awake() => Init();

    private void OnEnable() => SubscribeEnvets();

    private void Update() => HandleControl();

    private void OnDisable() => UnsubscribeEvents();


    private void Init()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        CurrentHp.Value = MaxHp;
    }

    private void HandleControl()
    {
        if(!_isActivateControl) return;

        HandleMove();
    }

    private void HandleMove()
    {
        if (_targetTransform == null) return;

        //_navMeshAgent.isStopped = true;

        if (_canTracking)
        {
            _navMeshAgent.SetDestination(_targetTransform.position);
        }

        _navMeshAgent.isStopped = !_canTracking;

        if(_navMeshAgent.velocity.magnitude > 0.1f)
        {
            IsMoving.Value = true;
        }
        else
        {
            IsMoving.Value = false;
        }

        Attack();
    }

    private void Attack()
    {
        if (_targetTransform == null) return;
        
        if (Physics.OverlapSphereNonAlloc(transform.position, 10f, _searchTargets, _layerMask) > 0)
        {
            _navMeshAgent.SetDestination(_searchTargets[0].transform.position);

            if (Physics.OverlapSphereNonAlloc(transform.position, 1f, _traceTarget, _layerMask) > 0)
            {
                _navMeshAgent.isStopped = true;
                IsAttacking.Value = true;
                if (_canAttack)
                    _atkCoroutine = StartCoroutine(AttackPlayer());
            }
            else
            {
                _navMeshAgent.isStopped = false;
                IsAttacking.Value = false;
            }
        }
        else
        {
            _navMeshAgent.SetDestination(_targetTransform.position);
        }
    }

    private void SearchTarget()
    {
        if (_targetTransform == null)
        {
            _canTracking = false;
            _navMeshAgent.isStopped = true;
        }

        if (_targetTransform != null)
        {
            _canTracking = true;
            _navMeshAgent.isStopped = false;
        }
    }

    public void TakeDamage(int value)
    {
        CurrentHp.Value -= value;
        if (CurrentHp.Value <= 0)
        {
            CurrentHp.Value = 0;
            Destroy(gameObject);
            // Handle death
            Debug.Log("Monster is dead");
        }
    }

    private void SetHpGaugeUI(int currentHp)
    {
        float hp = (float)currentHp / MaxHp;
        _hpGaugeUI.SetImageFillAmount(hp);
    }


    public void SubscribeEnvets()
    {
        IsAttacking.Subscribe(AttackAnim);
        IsMoving.Subscribe(MoveAnim);
        CurrentHp.Subscribe(SetHpGaugeUI);
    }

    public void UnsubscribeEvents()
    {
        IsAttacking.Unsubscribe(AttackAnim);
        IsMoving.Unsubscribe(MoveAnim);
        CurrentHp.Unsubscribe(SetHpGaugeUI);
    }


    private void MoveAnim(bool value)
    {
        _animator.SetBool("IsMoving", value);
    }

    private void AttackAnim(bool value)
    {
        _animator.SetBool("IsAttacking", value);
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(1f);

        if (Physics.OverlapSphereNonAlloc(transform.position, 1f, _traceTarget, _layerMask) > 0)
        {
            _traceTarget[0].GetComponent<PlayerController>()?.TakeDamage(30);
        }

        _atkCoroutine = null;
    }
}
