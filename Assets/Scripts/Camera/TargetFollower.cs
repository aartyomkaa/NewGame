using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Vector3 _cameraOffset;

    private void Update()
    {
        Vector3 newPosition = _targetTransform.position + _cameraOffset;
        transform.position = newPosition;
    }
}