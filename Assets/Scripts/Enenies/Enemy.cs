using System.Resources;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100; // Vie maximale
    public int health; // Vie actuelle
    public int damage = 10; // D�g�ts inflig�s au joueur
    public int reward = 10; // Ressources octroy�es au joueur � la mort
    public float speed = 2f; // Vitesse de d�placement
    public Animator animator; // Animation de l'ennemi

    private bool isDead = false;

    void Start()
    {
        health = maxHealth;
    }

    // Applique des d�g�ts � l'ennemi
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Ne pas infliger de d�g�ts si d�j� mort

        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            // Animation de d�g�ts (optionnelle)
            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }
        }
    }

    // G�re la mort de l'ennemi
    protected virtual void Die()
    {
        isDead = true;

        // Animation de mort
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // D�sactive le collider et le d�placement
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        // Octroie des ressources au joueur
        //ResourceManager.Instance.AddResources(reward);

        // D�truit l'objet apr�s un d�lai (pour laisser l'animation se jouer)
        Destroy(gameObject, 2f);
    }

    // D�place l'ennemi vers la cible
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