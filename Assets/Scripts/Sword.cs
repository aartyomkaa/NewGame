using UnityEngine;

public abstract class Sword : MonoBehaviour
{
    [SerializeField] protected int Damage;

    protected Collider _collider;

    protected void Start()
    {
        _collider = GetComponent<Collider>();

        _collider.enabled = false;
    }

    protected abstract void OnTriggerEnter(Collider collision);

    public void EnableCollider()
    {
        _collider.enabled = true;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
}