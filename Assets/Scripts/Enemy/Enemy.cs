using System;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    public event Action OnDeath;

    protected int MaxHealth;
    protected int Health;
    protected float MoveSpeed;

    [SerializeField]
    private Image _healthBar;

    protected Rigidbody2D Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(int health, float moveSpeed)
    {
        MaxHealth = health;
        Health = MaxHealth;
        MoveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Vector2.down * MoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyFinishLine finishLine = other.gameObject.GetComponent<EnemyFinishLine>();
        if (finishLine)
        {
            finishLine.PlayerHealth.ApplyDamage(1);
            Destroy(gameObject);
        }
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0) throw new ArgumentOutOfRangeException("damage < 0");
        Health -= damage > Health ? Health : damage;
        _healthBar.fillAmount = (float)Health / (float)MaxHealth;
        if (Health == 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

}
