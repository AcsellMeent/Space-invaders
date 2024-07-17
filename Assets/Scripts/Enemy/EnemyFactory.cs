using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType
{
    Alien,
}

public class EnemyFactory
{
    private Transform[] _spawnPoints;

    public EnemyFactory(Transform[] spawnPoints)
    {
        _spawnPoints = spawnPoints;
    }

    public Enemy CreateEnemy(EnemyType enemyType, int health, float moveSpeed)
    {
        if(_spawnPoints.Length == 0) throw new Exception("spawnPoints does not exist");
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Enemy enemy;
        switch (enemyType)
        {
            case EnemyType.Alien:
                enemy = GameObject.Instantiate(Resources.Load<Alien>("Prefabs/Enemies/Alien"), spawnPoint.position, Quaternion.identity, spawnPoint);
                break;
            default:
                throw new ArgumentException("Unknown enemy type");
        }
        enemy.Init(health, moveSpeed);
        return enemy;
    }
}
