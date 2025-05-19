using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamagable
{
    [SerializeField] private InputAction _testKey;

    public bool IsControlActivate { get; set; } = true;

    private PlayerStatus _status;
    private PlayerMovement _movement;
    private PlayerAnimation _animation;
    private InputAction _aimInputAction;
    private InputAction _shootInputAction;

    [SerializeField] private CinemachineVirtualCamera _aimCamera;
    [SerializeField] private Gun _gun;
    [SerializeField] private HpGaugeUI _hpGaugeUI;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEnvets();
    private void Update() => HandlePlayerControl();
    private void OnDisable() => UnsubscribeEvents();

    private void Init()
    {
        _movement = GetComponent<PlayerMovement>();
        _status = GetComponent<PlayerStatus>();
        _animation = GetComponent<PlayerAnimation>();
        _aimInputAction = GetComponent<PlayerInput>().actions["Aim"];
        _shootInputAction = GetComponent<PlayerInput>().actions["Shoot"];

        _hpGaugeUI.SetImageFillAmount(1);
        _status.CurrentHp.Value = _status.MaxHp;
    }

    private void HandlePlayerControl()
    {
        if (!IsControlActivate) return;

        HandleMovement();
        // HandleAiming();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if(_status.IsAiming.Value && _shootInputAction.IsPressed())
        {
            _status.IsAttacking.Value = _gun.Shoot();
        }
        else
        {
            _status.IsAttacking.Value = false;
        }
    }

    private void HandleMovement()
    {
        Vector3 camRotateDir = _movement.SetAimRotation();

        float moveSpeed;
        if (_status.IsAiming.Value) moveSpeed = _status.WalkSpeed;
        else moveSpeed = _status.RunSpeed;

        Vector3 moveDir = _movement.SetMove(moveSpeed);
        _status.IsMoving.Value = (moveDir != Vector3.zero);

        Vector3 avatarDir;
        if (_status.IsAiming.Value) avatarDir = camRotateDir;
        else avatarDir = moveDir;

        _movement.SetAvatarRotation(avatarDir);

        if (_status.IsAiming.Value)
        {
            _animation.SetX(_movement.InputDirection.x);
            _animation.SetZ(_movement.InputDirection.y);
        }
    }

    private void HandleAiming(InputAction.CallbackContext ctx)
    {
        // _status.IsAiming.Value = Input.GetKey(_aimKey);
        _status.IsAiming.Value = ctx.started;
    }

    public void TakeDamage(int value)
    {
        _status.CurrentHp.Value -= value;

        if(_status.CurrentHp.Value <= 0) Dead();
    }

    public void RecoveryHp(int value)
    {
        int hp = _status.CurrentHp.Value + value;
        _status.CurrentHp.Value = Mathf.Clamp(hp, 0, _status.MaxHp);
    }

    public void Dead()
    {
        Debug.Log("플레이어 사망");
    }

    public void SubscribeEnvets()
    {
        _status.IsAiming.Subscribe(_aimCamera.gameObject.SetActive);
        _status.IsAiming.Subscribe(_animation.SetAiming);
        _status.IsMoving.Subscribe(_animation.SetMove);
        _status.IsAttacking.Subscribe(_animation.SetAttack);
        _status.CurrentHp.Subscribe(SetHpGaugeUI);

        _aimInputAction.Enable();
        _aimInputAction.started += HandleAiming;
        _aimInputAction.canceled += HandleAiming;
    }

    public void UnsubscribeEvents()
    {
        _status.IsAiming.Unsubscribe(_aimCamera.gameObject.SetActive);
        _status.IsAiming.Unsubscribe(_animation.SetAiming);
        _status.IsMoving.Unsubscribe(_animation.SetMove);
        _status.IsAttacking.Unsubscribe(_animation.SetAttack);
        _status.CurrentHp.Unsubscribe(SetHpGaugeUI);

        _aimInputAction.Disable();
        _aimInputAction.started -= HandleAiming;
        _aimInputAction.canceled -= HandleAiming;
    }

    private void SetHpGaugeUI(int currentHp)
    {
        float hp = (float)currentHp / _status.MaxHp;
        _hpGaugeUI.SetImageFillAmount(hp);
    }
}
