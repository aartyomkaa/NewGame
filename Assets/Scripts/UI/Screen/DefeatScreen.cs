using UnityEngine;
using UnityEngine.Events;

public class DefeatScreen : Screen
{
    [SerializeField] private AudioSource _audioSource;

    public event UnityAction<DefeatScreen> RestartButtonClick;

    public override void Close()
    {
        CanvasGroup.alpha = 0f;

        _audioSource.Stop();

        DeactivateScreen();
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1f;

        _audioSource.Play();

        ActivateScreen();
    }

    protected override void OnButtonClick()
    {
        RestartButtonClick?.Invoke(this);
    }
}