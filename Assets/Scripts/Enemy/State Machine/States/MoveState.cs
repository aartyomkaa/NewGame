using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    private float _speed;

    private float _minSpread = -0.5f;
    private float _maxSpread = 0.5f;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);

        LookAtTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            MoveAway();
        }
    }

    private void LookAtTarget()
    {
        Vector3 relativePosition = Target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
    }

    private void MoveAway()
    {
        float random = Random.Range(_minSpread, _maxSpread);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + random, transform.position.y, transform.position.z - random), _speed * Time.deltaTime);
    }
}