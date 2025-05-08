using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int _damage = 10;
    public float _lifeTime = 2f;
    private Vector3 _lastPosition;

    void Start()
    {
        _lastPosition = transform.position;
        Destroy(gameObject, _lifeTime);
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
                AimPoint target = hit.transform.GetComponent<AimPoint>();
                if (target != null)
                {
                    target.TakeDamage(_damage);
                    Debug.Log("Raycast hit: " + hit.transform.name);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Fallback, au cas où
        AimPoint target = collision.transform.GetComponent<AimPoint>();
        if (target != null)
        {
            target.TakeDamage(_damage);
            Debug.Log("Collision hit: " + collision.transform.name);
        }

        Destroy(gameObject);
    }
}
