using System.Collections;
using UnityEngine;

public class BulletDestroyParticle : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime;
    private void Awake()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
