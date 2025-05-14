using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _avatar;
    [SerializeField] private Transform _aim;

    private Rigidbody _rigidbody;
    private PlayerStatus _playerStatus;

    [Header("Mouse Config")]
    [SerializeField][Range(-90, 0)] private float _minPitch;
    [SerializeField][Range(0, 90)] private float _maxPitch;
    [SerializeField][Range(0, 5)] private float _mouseSensitivity = 1;

    private Vector2 _currentRotation;

    private void Awake() => Init();

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>();
    }

    public Vector3 SetMove(float moveSpeed)
    {
        Vector3 moveDiretion = GetMoveDirection();

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = moveDiretion.x * moveSpeed;
        velocity.z = moveDiretion.z * moveSpeed;

        _rigidbody.velocity = velocity;

        return moveDiretion;
    }

    public Vector3 SetAimRotation()
    {
        Vector2 mouseDir = GetMouseDirection();


        _currentRotation.x += mouseDir.x;

        _currentRotation.y = Mathf.Clamp(
            _currentRotation.y + mouseDir.y, 
            _minPitch, 
            _maxPitch
            );

        transform.rotation = Quaternion.Euler(0, _currentRotation.x, 0);

        Vector3 currrentEuler = _aim.localEulerAngles;
        _aim.localEulerAngles = new Vector3(_currentRotation.y, currrentEuler.y, currrentEuler.z);

        Vector3 rotationDirVector = transform.forward;
        rotationDirVector.y = 0;
        return rotationDirVector.normalized;
    }

    public void SetAvatarRotation(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        _avatar.rotation = Quaternion.Lerp(
            _avatar.rotation,
            targetRotation,
            _playerStatus.RotateSpeed * Time.deltaTime
            );
    }

    private Vector2 GetMouseDirection()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;

        return new Vector2(mouseX, mouseY);
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 input = GetInputDiretion();

        Vector3 direction = 
            (transform.right * input.x) + 
            (transform.forward * input.z);

        return direction.normalized;
    }

    public Vector3 GetInputDiretion()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        return new Vector3(x, 0, z);
    }
}
