using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour
{
    public float _reloadTime = 1f; // Time taken to reload
    public bool _isReloading = false; // Flag to check if reloading is in progress
    public int _currentAmmo = -1;
    public int _maxAmmo = 10;
    public int _price = 0;

    [SerializeField] protected Animator _gunAnimator;
    [SerializeField] protected AudioSource _shootSound;
    [SerializeField] protected  AudioClip _emptyClip;
    [SerializeField] protected AudioClip _impactClip;

    protected bool _canShoot = true;

    public void Start()
    {
        if (_currentAmmo == -1)
        {
            _currentAmmo = _maxAmmo; // Initialize current ammo
        }
    }

    public void OnEnable()
    {
        _isReloading = false; // Reset reloading flag when the gun is enabled
        _canShoot = true;
        _gunAnimator.SetBool("Reloading", false); // Reset reload animation
    }

    public void Update()
    {
        if (_isReloading) return; // Skip update if reloading

        if (_currentAmmo <= 0)
        {
            //je voudrais que le joueur ne puisse plus pouvoir tirer
            _canShoot = false; // Disable shooting if ammo is empty
        }
    }

    public void StartReload()
    {
        if (!_isReloading && _currentAmmo < _maxAmmo)
            StartCoroutine(Reload());
    }


    public abstract void Shoot();

    IEnumerator Reload()
    {
        _isReloading = true; // Set reloading flag
        Debug.Log("Reloading...");

        _gunAnimator.SetBool("Reloading", true); // Trigger reload animation

        _canShoot = false; // Disable shooting during reload
        yield return new WaitForSeconds(_reloadTime - .25f); // Wait for the reload time

        _gunAnimator.SetBool("Reloading", false); // Reset reload animation

        yield return new WaitForSeconds(.25f); // Wait for the reload animation to finish

        _currentAmmo = _maxAmmo; // Refill ammo
        _isReloading = false; // Reset reloading flag
        _canShoot = true; // Re-enable shooting after reload time
    }

    public bool CanShoot()
    {
        return _canShoot && !_isReloading && _currentAmmo > 0;
    }
}
