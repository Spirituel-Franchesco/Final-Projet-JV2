using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public int currentAmmo = -1;
    public int maxAmmo = 10;
    public float reloadTime = 1f; // Time taken to reload
    public int price = 0;

    protected bool canShoot = true;

    [SerializeField] protected Animator gunAnimator;

    private void Start()
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

    public abstract void Shoot();

    public virtual void Reload()
    {
        Debug.Log("Reloading...");
        currentAmmo = maxAmmo;
    }

    //protected void PlayShootAnimation()
    //{
    //    if (gunAnimator != null)
    //        gunAnimator.SetTrigger("Shoot");
    //}
}
