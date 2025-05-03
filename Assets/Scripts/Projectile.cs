using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 2f;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        CheckCollision();
        lastPosition = transform.position;
    }

    private void CheckCollision()
    {
        Vector3 direction = transform.position - lastPosition;
        float distance = direction.magnitude;

        if (distance > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(lastPosition, direction.normalized, out hit, distance))
            {
                AimPoint target = hit.transform.GetComponent<AimPoint>();
                if (target != null)
                {
                    target.TakeDamage(damage);
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
            target.TakeDamage(damage);
            Debug.Log("Collision hit: " + collision.transform.name);
        }

        Destroy(gameObject);
    }
}
