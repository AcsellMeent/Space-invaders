using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class GameRule : MonoBehaviour
{
    public event Action<bool> OnGameOver;

    [SerializeField]
    private int _maximumEnemyCount;

    [SerializeField]
    private int _minimumEnemyCount;

    private int _enemyCount;

    private EnemySpawner _enemySpawner;
    private PlayerHealth _playerHealth;

    private List<Enemy> _enemyList;

    private void Awake()
    {
        _enemyList = new List<Enemy>();
        _enemyCount = Random.Range(_minimumEnemyCount, _maximumEnemyCount + 1);
    }

    [Inject]
    public void Construct(EnemySpawner enemySpawner, PlayerHealth playerHealth)
    {
        _enemySpawner = enemySpawner;
        _enemySpawner.OnEnemySpawned += OnEnemySpawned;
        _playerHealth = playerHealth;
        _playerHealth.OnDeath += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        _enemySpawner.OnEnemySpawned -= OnEnemySpawned;
        foreach (Enemy enemy in _enemyList)
        {
            enemy.OnDeath -= OnEnemyDeath;
        }
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.OnDeath += OnEnemyDeath;
        _enemyList.Add(enemy);
    }

    private void OnPlayerDeath()
    {
        GameOver(false);
    }


    private void OnEnemyDeath()
    {
        _enemyCount--;
        if (_enemyCount == 0)
        {
            GameOver(true);
        }
    }

    private void GameOver(bool status)
    {
        OnGameOver?.Invoke(status);
        Time.timeScale = 0;
    }

    private void OnValidate()
    {
        ValidateIntRange(ref _minimumEnemyCount, ref _maximumEnemyCount);
    }

    private void ValidateIntRange(ref int minimum, ref int maximum)
    {
        if (minimum <= 0)
        {
            minimum = 1;
        }

        if (minimum >= maximum)
        {
            maximum = minimum + 1;
        }
    }
}
