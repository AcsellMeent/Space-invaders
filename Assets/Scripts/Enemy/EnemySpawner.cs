using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public event Action<Enemy> OnEnemySpawned;

    [SerializeField]
    private Transform[] _spawnPoints;

    [SerializeField]
    private float _maximumSpawnTimeout;

    [SerializeField]
    private float _minimumSpawnTimeout;

    [SerializeField]
    private float _maximumEnemySpeed;

    [SerializeField]
    private float _minimumEnemySpeed;

    [SerializeField]
    private int _enemyHealth;

    private EnemyFactory _enemyFactory;

    private void Awake()
    {
        _enemyFactory = new EnemyFactory(_spawnPoints);
        StartCoroutine(SpawnEnemy());
    }

    private void OnValidate()
    {
        ValidateFloatRange(ref _minimumSpawnTimeout, ref _maximumSpawnTimeout);
        ValidateFloatRange(ref _minimumEnemySpeed, ref _maximumEnemySpeed);
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(_minimumSpawnTimeout, _maximumSpawnTimeout));
        Enemy enemy = _enemyFactory.CreateEnemy(EnemyType.Alien, _enemyHealth, Random.Range(_minimumEnemySpeed, _maximumEnemySpeed));
        OnEnemySpawned?.Invoke(enemy);
        StartCoroutine(SpawnEnemy());
    }
    private void ValidateFloatRange(ref float minimum, ref float maximum)
    {
        if (minimum <= 0)
        {
            minimum = 1;
        }

        if (minimum >= maximum)
        {
            maximum = Mathf.Floor(minimum + 1);
        }
    }
}
