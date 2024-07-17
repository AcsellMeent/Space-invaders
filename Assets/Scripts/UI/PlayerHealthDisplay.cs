using TMPro;
using UnityEngine;
using Zenject;

public class PlayerHealthDisplay : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    [SerializeField]
    private string _healthLableFormat;

    [SerializeField]
    private TMP_Text _healthLable;

    [Inject]
    public void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
        _playerHealth.OnHealthChange += OnHealthChange;
        _healthLable.text = string.Format(_healthLableFormat, _playerHealth.Health);
    }

    private void OnDestroy()
    {
        _playerHealth.OnHealthChange -= OnHealthChange;
    }

    private void OnHealthChange(int health)
    {
        _healthLable.text = string.Format(_healthLableFormat, health);
    }
}
