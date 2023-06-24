using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float _attackDelay;
    [SerializeField] private Sword _sword;
    [SerializeField] private float _attackTime;

    private PlayerInput _playerInput;
    private Animator _animator;
    private AudioSource _audioSource;

    private Coroutine _attackDelayCoroutine;
    private Coroutine _attackTimeCoroutine;

    private bool _hasAttacked;
    private int _animatorAttack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();

        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _playerInput.Player.Attack.performed += ctx => OnAttack();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnAttack()
    {
        if (_hasAttacked == false)
        {
            SetSwordCollider();

            if(_attackDelayCoroutine != null)
            {
                StopCoroutine(_attackDelayCoroutine);
            }

            _animator.SetTrigger(_animatorAttack);
            _audioSource.Play();

            _attackDelayCoroutine = StartCoroutine(AttackDelay(_attackDelay));
        }
    }

    private void SetSwordCollider()
    {
        if (_attackTimeCoroutine != null)
        {
            StopCoroutine(_attackTimeCoroutine);
        }

        _sword.EnableCollider();

        _attackTimeCoroutine = StartCoroutine(AttackTime(_attackTime));
    }

    private IEnumerator AttackDelay(float attackDelay)
    {
        _hasAttacked = true;

        var waitSeconds = new WaitForSeconds(attackDelay);

        yield return waitSeconds;

        _hasAttacked = false;
    }

    private IEnumerator AttackTime(float attackTime)
    {
        var waitSeconds = new WaitForSeconds(attackTime);

        yield return waitSeconds;

        _sword.DisableCollider();
    }
}