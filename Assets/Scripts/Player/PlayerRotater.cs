using UnityEngine;

public class PlayerRotater : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Player _player;

    private Ray _ray;
    private float _rayDistance = 30f;
    private Vector3 _lookDirection;
    private BoxCollider _playerCollider;

    private void Start()
    {
        _playerCollider = _player.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _layerMask) && hit.collider != _playerCollider)
        {
            _lookDirection = hit.point - transform.position;

            _player.transform.rotation = Quaternion.LookRotation(_lookDirection, Vector3.up);
        }
    }
}