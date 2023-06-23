using UnityEngine;
using UnityEngine.Events;

public class StartScreen : Screen
{
    public event UnityAction PlayButtonClick;

    public override void Close()
    {
        CanvasGroup.alpha = 0f;

        DeactivateScreen();
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1f;

        ActivateScreen();
    }

    protected override void OnButtonClick()
    {
        PlayButtonClick?.Invoke();
    }
}