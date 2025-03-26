using UnityEngine;

public class Gun : MonoBehaviour
{
    public int _ammo = 10; // Munitions actuelles
    public int _maxAmmo = 10; // Munitions maximum
    public int _damage = 10; // D�g�ts par tir
    public float _recoilStrength = 0.1f; // Force du recul
    public float _fireRate = 0.5f; // Temps entre les tirs
    public Animator _animator; // Animation de recul

    private float nextFireTime = 0f;

    // Fonction de tir de base
    public virtual void Shoot()
    {
        if (Time.time >= nextFireTime && _ammo > 0)
        {
            _ammo--; // Consomme une munition
            nextFireTime = Time.time + _fireRate; // D�finit le prochain temps de tir

            // Joue l'animation de recul
            if (_animator != null)
            {
                _animator.SetTrigger("Recoil");
            }

            // Applique le recul (optionnel)
            ApplyRecoil();

            // G�re le tir (� impl�menter dans les classes enfants)
            HandleShoot();
        }
        else if (_ammo <= 0)
        {
            Debug.Log("Plus de munitions !");
        }
    }

    // Applique un effet de recul (optionnel)
    protected virtual void ApplyRecoil()
    {
        // Exemple : d�place l�g�rement l'arme vers l'arri�re
        transform.localPosition -= transform.forward * _recoilStrength;
    }

    // Fonction � override dans les classes enfants pour g�rer le tir
    protected virtual void HandleShoot()
    {
        Debug.Log("Tir de base");
    }
}