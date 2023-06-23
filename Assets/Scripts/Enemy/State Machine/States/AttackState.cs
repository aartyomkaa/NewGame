using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    [SerializeField] private float _delay;
    [SerializeField] private Sword _sword;
    [SerializeField] private float _attackTime;

    private float _minSpread = -0.5f;
    private float _maxSpread = 0.5f;

    private Animator _animator;
    private AudioSource _audioSource;
    private Coroutine _attackTimeCoroutine;

    private int _animatorAttack = Animator.StringToHash("Attack");
    private float _lastAttackTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(Target);

            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            float random = Random.Range(_minSpread, _maxSpread);

            transform.position =  Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + random, transform.position.y, transform.position.z + random), 4 * Time.deltaTime);
        }
    }

    private void Attack(Player player)
    {
        SetSwordCollider();

        _audioSource.Play();

        _animator.SetTrigger(_animatorAttack);
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