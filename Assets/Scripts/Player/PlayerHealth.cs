using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action<int> OnHealthChange;
    public event Action OnDeath;

    public int _health;
    public int Health { get => _health; }

    public void ApplyDamage(int damage)
    {
        if (damage < 0) throw new ArgumentOutOfRangeException("damage < 0");

        _health -= damage > _health ? _health : damage;
        OnHealthChange?.Invoke(_health);
        if (_health == 0)
        {
            OnDeath?.Invoke();
        }
    }
}
