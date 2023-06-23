using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _hitRecoveryTime;

    public event UnityAction Died;
    public event UnityAction FellOut;
    public event UnityAction<int, int> HealthChanged;

    private Animator _animator;
    private int _animatorDeadBool = Animator.StringToHash("Dead");

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _hasTookHit;
    private float _deadAnimationTime = 1f;
    private int _maxHeaalth;

    private Coroutine _hitRecovery;
    private Coroutine _diedCoroutine;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _startPosition = transform.position;
        _startRotation = transform.rotation;

        _maxHeaalth = _health;
    }

    public void TakeHit(int damage)
    {
        if (_hasTookHit)
        {
            return;
        }

        _health -= damage;

        HealthChanged?.Invoke(_health, _maxHeaalth);

        if (_health <= 0)
        {
            Die();
        }

        if (_hitRecovery != null)
        {
            StopCoroutine(_hitRecovery);
        }

        _hitRecovery = StartCoroutine(HitRecovery());
    }

    public void FallOut()
    {
        FellOut?.Invoke();
    }

    public void ResetPlayer()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;

        _health = _maxHeaalth;
        HealthChanged?.Invoke(_health, _maxHeaalth);

        _animator.SetBool(_animatorDeadBool, false);
    }

    private void Die()
    {
        _animator.SetBool(_animatorDeadBool, true);

        if (_diedCoroutine != null)
        {
            StopCoroutine(DeadTimer());
        }

        _diedCoroutine = StartCoroutine(DeadTimer());
    }

    private IEnumerator DeadTimer()
    {
        float timePassed = 0;

        while (timePassed < _deadAnimationTime) 
        {
            timePassed += Time.deltaTime;

            yield return Time.deltaTime;
        }

        Died?.Invoke();
    }

    private IEnumerator HitRecovery()
    {
        _hasTookHit = true;

        float timePassed = 0;

        while (timePassed < _hitRecoveryTime)
        {
            timePassed += Time.deltaTime;

            yield return Time.deltaTime;
        }

        _hasTookHit = false;
    }
}
