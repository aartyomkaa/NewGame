using UnityEngine;
using UnityEngine.Events;

public class VictoryScreen : Screen
{
    public event UnityAction<VictoryScreen> RestartButtonClick;

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
        RestartButtonClick?.Invoke(this);
    }
}