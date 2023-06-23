using UnityEngine;

public class PlayerHealthBar : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        Slider.value = 1;

        _player.HealthChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnValueChanged;
    }
}