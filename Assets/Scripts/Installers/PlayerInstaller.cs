using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Transform _playerSpawnPoint;

    public override void InstallBindings()
    {
        var playerInstance = Container.InstantiatePrefab(_player, _playerSpawnPoint);

        PlayerHealth playerHealth = playerInstance.GetComponent<PlayerHealth>();

        Container.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle().NonLazy();
    }
}
