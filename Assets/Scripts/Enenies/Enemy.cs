using System.Resources;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100; // Vie maximale
    public int health; // Vie actuelle
    public int damage = 10; // Dégâts infligés au joueur
    public int reward = 10; // Ressources octroyées au joueur à la mort
    public float speed = 2f; // Vitesse de déplacement
    public Animator animator; // Animation de l'ennemi

    private bool isDead = false;

    void Start()
    {
        health = maxHealth;
    }

    // Applique des dégâts à l'ennemi
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Ne pas infliger de dégâts si déjà mort

        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            // Animation de dégâts (optionnelle)
            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }
        }
    }

    // Gère la mort de l'ennemi
    protected virtual void Die()
    {
        isDead = true;

        // Animation de mort
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Désactive le collider et le déplacement
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        // Octroie des ressources au joueur
        //ResourceManager.Instance.AddResources(reward);

        // Détruit l'objet après un délai (pour laisser l'animation se jouer)
        Destroy(gameObject, 2f);
    }

    // Déplace l'ennemi vers la cible
    public void MoveTowards(Vector3 targetPosition)
    {
        if (isDead) return; // Ne pas bouger si mort

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition); // Oriente l'ennemi vers la cible

        // Animation de marche/course
        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
        }
    }
}