using System.Collections;
using UnityEngine;

public class PlayerAutoShooting : MonoBehaviour
{
    [Tooltip("Shots per second")]
    [SerializeField]
    private float _shootingSpeed;

    [SerializeField]
    private float _shootingRadius;

    [SerializeField]
    private int _bulletDamage;

    [SerializeField]
    private float _bulletSpeed;


    [SerializeField]
    private Bullet _bullet;

    [SerializeField]
    private LayerMask _layerMask;

    private Transform _currentTarget;

    [SerializeField]
    private Transform _bulletSpawnPoint;

    [SerializeField]
    private Transform _playerModel;

    private bool _canShoot;

    private void Awake()
    {
        _canShoot = true;
    }

    private void Update()
    {
        if (_currentTarget)
        {
            if (_canShoot)
            {
                _canShoot = false;
                Vector2 direction = (_currentTarget.position - transform.position).normalized;
                //Раскомментируйте если хотите чтобы корабль поворачивался в сторону выстрела
                // _playerModel.up = direction;
                Bullet bullet = Instantiate(_bullet, _bulletSpawnPoint.position, Quaternion.identity);
                bullet.transform.up = direction;
                bullet.Init(_bulletDamage, _bulletSpeed);
                StartCoroutine(Reload());
            }
        }

        Aim();
    }

    private void Aim()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _shootingRadius, _layerMask);

        float nearestDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.GetComponent<Enemy>())
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    _currentTarget = collider.transform;
                }
            }
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1 / _shootingSpeed);
        _canShoot = true;
    }
}
