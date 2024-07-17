using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    private int _damage;
    private float _speed;

    [SerializeField]
    private GameObject _destroyParticle;

    [SerializeField]
    private LayerMask _boundsLayer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(int damage, float speed)
    {
        _damage = damage;
        _speed = speed;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_boundsLayer & (1 << other.gameObject.layer)) != 0)
        {
            OnBulletDestroy();
        }

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.ApplyDamage(_damage);
            OnBulletDestroy();
        }
    }

    private void OnBulletDestroy()
    {
        Instantiate(_destroyParticle, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }
}
