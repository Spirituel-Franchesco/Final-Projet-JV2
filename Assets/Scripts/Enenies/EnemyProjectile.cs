using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int _damage = 10; // Dégâts infligés au héros

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero")) // Assure-toi que ton héros a le tag "Hero"
        {
            HeroHealth._Instance.TakeDamage(_damage); // Infliger les dégâts
            Destroy(gameObject); // Détruire le projectile après collision
        }
    }
}
