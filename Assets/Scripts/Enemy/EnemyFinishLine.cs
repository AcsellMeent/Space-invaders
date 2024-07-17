using UnityEngine;
using Zenject;

public class EnemyFinishLine : MonoBehaviour
{
    public PlayerHealth PlayerHealth { get; private set; }

    [Inject]
    public void Construct(PlayerHealth playerHealth)
    {
        PlayerHealth = playerHealth;
    }
}
