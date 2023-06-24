using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerRotater _playerRotater;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private DefeatScreen _defeatScreen;
    [SerializeField] private VictoryScreen _victoryScreen;

    private Enemy _boss;

    private void OnEnable()
    {
        _startScreen.PlayButtonClick += OnPlayButtonClick;

        _defeatScreen.RestartButtonClick += OnRestartButtonClick;
        _victoryScreen.RestartButtonClick += OnRestartButtonClick;

        _player.Died += OnDefeat;
        _player.FellOut += OnDefeat;

        _enemySpawner.BossSpawned += OnBossSpawned;
    }

    private void Start()
    {
        _defeatScreen.Close();
        _victoryScreen.Close();
        _gameScreen.Close();

        Time.timeScale = 0f;
        _startScreen.Open();
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonClick;

        _defeatScreen.RestartButtonClick -= OnRestartButtonClick;
        _victoryScreen.RestartButtonClick -= OnRestartButtonClick;

        _player.Died -= OnDefeat;
        _player.FellOut -= OnDefeat;

        _enemySpawner.BossSpawned -= OnBossSpawned;
        _boss.Died -= OnVictory;
    }

    private void OnVictory()
    {
        Time.timeScale = 0f;
        _playerRotater.enabled = false;

        _gameScreen.Close();
        _victoryScreen.Open();
    }

    private void OnDefeat()
    {
        Time.timeScale = 0f;
        _playerRotater.enabled = false;

        _gameScreen.Close();
        _defeatScreen.Open();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        _gameScreen.Open();
        
        StartGame();
    }

    private void OnRestartButtonClick(Screen screen)
    {
        screen.Close();
        _gameScreen.Open();

        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1.0f;
        _player.ResetPlayer();
        _playerRotater.enabled = true;

        _enemySpawner.Restart();
    }

    private void OnBossSpawned(Enemy enemy)
    {
        _boss = enemy;

        _boss.Died += OnVictory;
    }
}