using UnityEngine;

public class DistanceToAttackTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

    private void Start()
    {
        _transitionRange += Random.Range(_rangeSpread, -_rangeSpread);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}