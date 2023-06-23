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

    private float _timePassed;
    private int _animatorAttack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();

        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _playerInput.Player.Attack.performed += ctx => OnAttack();

        _timePassed = _attackDelay;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnAttack()
    {
        if (_timePassed >= _attackDelay)
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
        _timePassed = 0;

        while (_timePassed < _attackDelay)
        {
            _timePassed += Time.deltaTime;

            yield return Time.deltaTime;
        }
    }

    private IEnumerator AttackTime(float attackTime)
    {
        float timePassed = 0;

        while (timePassed < _attackTime)
        {
            timePassed += Time.deltaTime;

            yield return Time.deltaTime;
        }

        _sword.DisableCollider();
    }
}