using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsControlActivate { get; set; } = true;

    private PlayerStatus _status;
    private PlayerMovement _movement;
    private PlayerAnimation _animation;

    [SerializeField] private CinemachineVirtualCamera _aimCamera;
    [SerializeField] private Gun _gun;
    [SerializeField] private KeyCode _aimKey = KeyCode.Mouse1;
    [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEnvets();
    private void Update() => HandlePlayerControl();
    private void OnDisable() => UnsubscribeEvents();

    private void Init()
    {
        _movement = GetComponent<PlayerMovement>();
        _status = GetComponent<PlayerStatus>();
        _animation = GetComponent<PlayerAnimation>();
    }

    private void HandlePlayerControl()
    {
        if (!IsControlActivate) return;

        HandleMovement();
        HandleAiming();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if(_status.IsAiming.Value && Input.GetKey(_shootKey))
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
            Vector3 input = _movement.GetInputDiretion();
            _animation.SetX(input.x);
            _animation.SetZ(input.z);
        }
    }

    private void HandleAiming()
    {
        _status.IsAiming.Value = Input.GetKey(_aimKey);
    }

    public void SubscribeEnvets()
    {
        _status.IsAiming.Subscribe(_aimCamera.gameObject.SetActive);
        _status.IsAiming.Subscribe(_animation.SetAiming);
        _status.IsMoving.Subscribe(_animation.SetMove);
        _status.IsAttacking.Subscribe(_animation.SetAttack);
    }

    public void UnsubscribeEvents()
    {
        _status.IsAiming.Unsubscribe(_aimCamera.gameObject.SetActive);
        _status.IsAiming.Unsubscribe(_animation.SetAiming);
        _status.IsMoving.Unsubscribe(_animation.SetMove);
        _status.IsAttacking.Unsubscribe(_animation.SetAttack);
    }
}
