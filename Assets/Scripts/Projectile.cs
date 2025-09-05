using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private int _damage = 10;

    private Vector3 _lastPosition;
    private ObjectPool _pool;

    public void Initialize(ObjectPool pool)
    {
        _pool = pool;
        _lastPosition = transform.position;
        Invoke(nameof(Deactivate), _lifeTime);

        Debug.Log($"[POOL] Projectile activated from pool at {transform.position}");
    }

    void Update()
    {
        CheckCollision();
        _lastPosition = transform.position;
    }

    private void CheckCollision()
    {
        Vector3 direction = transform.position - _lastPosition;
        float distance = direction.magnitude;

        if (distance > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(_lastPosition, direction.normalized, out hit, distance))
            {
                HeroHealth target = hit.transform.GetComponent<HeroHealth>();
                if (target != null)
                {
                    target.TakeDamage(_damage);
                    Debug.Log("Raycast hit: " + hit.transform.name);
                    Debug.Log("[POOL] Raycast hit hero: " + hit.transform.name);
                }

                Deactivate();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AimPoint target = collision.transform.GetComponent<AimPoint>();
        if (target != null)
        {
            target.TakeDamage(_damage);
            Debug.Log("Collision hit: " + collision.transform.name);
            Debug.Log("[POOL] Collision hit aim point: " + collision.transform.name);
        }

        Deactivate();
    }

    private void Deactivate()
    {
        CancelInvoke();
        if (_pool != null)
        {
            Debug.Log($"[POOL] Projectile returned to pool at {transform.position}");
            _pool.ReturnToPool(gameObject);
        }
        else
        {
            Debug.LogWarning("[POOL] Projectile destroyed instead of returned to pool!");
            Destroy(gameObject);
        }
    }
}
