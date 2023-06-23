using UnityEngine;

public class EnemyHealthBar : Bar
{
    [SerializeField] private Enemy _enemy;

    private Quaternion _rotation;

    private void OnEnable()
    {
        _rotation = Quaternion.identity;

        Slider.value = 1;

        _enemy.HealthChanged += OnValueChanged;
    }

    private void LateUpdate()
    {
        Slider.transform.rotation = _rotation;
    }

    private void OnDisable()
    {
        _enemy.HealthChanged -= OnValueChanged;
    }
}