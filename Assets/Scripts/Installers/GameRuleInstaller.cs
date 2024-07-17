using UnityEngine;
using Zenject;

public class GameRuleInstaller : MonoInstaller
{
    [SerializeField]
    private GameRule _gameRule;

    [SerializeField]
    private Transform _gameRuleRoot;

    [SerializeField]
    private EnemySpawner _enemySpawner;

    public override void InstallBindings()
    {
        Container.Bind<EnemySpawner>().FromComponentInHierarchy(_enemySpawner).AsSingle();

        var gameRuleInstance = Container.InstantiatePrefabForComponent<GameRule>(_gameRule, _gameRuleRoot);

        Container.Bind<GameRule>().FromInstance(gameRuleInstance).AsSingle().NonLazy();
    }
}
