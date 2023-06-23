using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Player _player;

    public event UnityAction<Enemy> BossSpawned;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfetLastSpawn;
    private int _spawned;

    private void Start()
    {
        SetWave(_currentWaveNumber); 
    }

    private void Update()
    {
        if (_currentWave == null)
        {
            return;
        }

        _timeAfetLastSpawn += Time.deltaTime;

        if (_timeAfetLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();

            _spawned++;
            _timeAfetLastSpawn = 0;
        }

        if(_currentWave.Count <= _spawned)
        {
            _currentWave = null;

            if(_waves.Count != _currentWaveNumber + 1)
            {
                NextWave();
            }
        }
    }

    public void Restart()
    {
        _currentWaveNumber = 0;

        for(int i = 0; i < _spawnPoints.Count; i++)
        {
            foreach(Transform child in _spawnPoints[i])
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        SetWave(_currentWaveNumber);
    }

    private void NextWave()
    {
        SetWave(++_currentWaveNumber);

        _spawned = 0;
    }

    private void InstantiateEnemy()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        Enemy enemy = Instantiate(_currentWave.Template, spawnPoint.position, spawnPoint.rotation, spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);

        if (_currentWave.IsBossWave)
        {
            BossSpawned?.Invoke(enemy);
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }
}

[System.Serializable]
public class Wave
{
    public Enemy Template;
    public float Delay;
    public int Count;
    public bool IsBossWave;
}