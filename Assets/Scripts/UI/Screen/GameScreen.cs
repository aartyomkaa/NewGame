using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private AudioSource _audioSource;

    public void Close()
    {
        _canvasGroup.alpha = 0f;
        _audioSource.Stop();
    }

    public void Open()
    {
        _canvasGroup.alpha = 1f;
        _audioSource.Play();
    }
}