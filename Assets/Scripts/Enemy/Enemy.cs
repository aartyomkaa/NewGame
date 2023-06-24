using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _hitRecoveryTime;
    [SerializeField] private ParticleSystem _deathEffect;

    private int _maxHeaalth;
    private bool _hasTookHit;

    private Player _target;
    private Coroutine _hitRecovery;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction Died;

    public Player Target => _target;

    private void Start()
    {
        _maxHeaalth = _health;
    }

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeHit(int damage)
    {
        if (_hasTookHit)
        {
            return;
        }

        _health -= damage;

        HealthChanged?.Invoke(_health, _maxHeaalth);

        if (_health <= 0 )
        {
            Die();
        }

        if (_hitRecovery != null)
        {
            StopCoroutine(_hitRecovery);
        }

        _hitRecovery = StartCoroutine(HitRecovery());
    }

    protected virtual void Die()
    {
        Died?.Invoke();

        Instantiate(_deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private IEnumerator HitRecovery()
    {
        _hasTookHit = true;

        var waitSeconds = new WaitForSeconds(_hitRecoveryTime);

        yield return waitSeconds;

        _hasTookHit = false;
    }
}