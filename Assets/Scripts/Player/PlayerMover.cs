using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Animator _animator;
    private PlayerInput _playerInput;
    private Vector2 _moveDirection;

    private int _animatorSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void Update()
    {
        _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();

        Move(_moveDirection);
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Move(Vector2 direction)
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 move = new Vector3(direction.x, 0, direction.y);

        SetAnimatorSpeed(move);

        transform.position += move * scaledMoveSpeed;
    }

    private void SetAnimatorSpeed(Vector3 move)
    {
        float speed = Vector3.Magnitude(move * _moveSpeed);

        _animator.SetFloat(_animatorSpeed, speed);
    }
}