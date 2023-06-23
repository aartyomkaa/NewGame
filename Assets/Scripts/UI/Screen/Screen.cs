using UnityEngine;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;
    [SerializeField] protected Button Button;
    [SerializeField] protected Button ExitButton;

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClick);
        ExitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected abstract void OnButtonClick();

    protected virtual void OnExitButtonClick()
    {
        Application.Quit();
    }

    protected virtual void DeactivateScreen()
    {
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    protected virtual void ActivateScreen()
    {
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    public abstract void Open();

    public abstract void Close();
}