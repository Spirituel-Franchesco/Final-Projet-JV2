using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour
{
    public int currentAmmo = -1;
    public int maxAmmo = 10;
    public float reloadTime = 1f; // Time taken to reload
    public int price = 0;

    protected bool canShoot = true;
    protected bool isReloading = false; // Flag to check if reloading is in progress


    [SerializeField] protected Animator gunAnimator;

    public void Start()
    {
        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo; // Initialize current ammo
        }
        //else
        //{
        //    currentAmmo = maxAmmo; // Initialize current ammo
        //}
        //currentAmmo = maxAmmo; // Initialize current ammo
    }

    public void OnEnable()
    {
        isReloading = false; // Reset reloading flag when the gun is enabled
        gunAnimator.SetBool("Reloading", false); // Reset reload animation
    }

    public void Update()
    {
        if (isReloading) return; // Skip update if reloading

        if (currentAmmo <= 0)
        {
            //Reload();
            StartCoroutine(Reload());
            return; // Reload if ammo is empty
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public abstract void Shoot();

    //public abstract void Reload();
    //{
    //    Debug.Log("Reloading...");
    //    currentAmmo = maxAmmo;
    //}

    //protected void PlayShootAnimation()
    //{
    //    if (gunAnimator != null)
    //        gunAnimator.SetTrigger("Shoot");
    //}

    IEnumerator Reload()
    {
        isReloading = true; // Set reloading flag
        Debug.Log("Reloading...");
        
        gunAnimator.SetBool("Reloading", true); // Trigger reload animation

        canShoot = false; // Disable shooting during reload
        yield return new WaitForSeconds(reloadTime - .25f); // Wait for the reload time

        gunAnimator.SetBool("Reloading", false); // Reset reload animation

        yield return new WaitForSeconds(.25f); // Wait for the reload animation to finish

        currentAmmo = maxAmmo; // Refill ammo
        isReloading = false; // Reset reloading flag
        canShoot = true; // Re-enable shooting after reload time
    }
}
