using System.Collections.Generic;
using UnityEngine;

public class PlayerGunSystem : MonoBehaviour
{
    //public int money = 200;

    //private Dictionary<string, bool> ownedGuns = new Dictionary<string, bool>();
    //private string currentGun = "Pistol"; // par défaut

    //public bool HasGun(string gunName) => ownedGuns.ContainsKey(gunName) && ownedGuns[gunName];

    //public void BuyGun(string gunName, int price)
    //{
    //    money -= price;
    //    ownedGuns[gunName] = true;
    //    currentGun = gunName;
    //    Debug.Log("Acheté : " + gunName);
    //}

    //public void SwitchGun(string gunName)
    //{
    //    currentGun = gunName;
    //    Debug.Log("Changement pour : " + gunName);
    //}

    //public void ReloadGun(int price)
    //{
    //    money -= price;
    //    Debug.Log("Reload de " + currentGun);
    //    // Ajoute la logique de recharge plus tard
    //}


    public Transform shootOrigin;
    public int money = 200;

    private Dictionary<string, Gun> ownedGuns = new();
    private Gun currentGun;

    public GameObject pistolPrefab;
    public GameObject riflePrefab;
    public GameObject shotgunPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (currentGun != null)
               // currentGun.Shoot(shootOrigin);
        }
    }

    public bool HasGun(string gunName)
    {
        return ownedGuns.ContainsKey(gunName);
    }

    public void BuyGun(string gunName, int price)
    {
        if (money < price) return;

        money -= price;
        GameObject prefab = GetGunPrefab(gunName);
        if (prefab == null) return;

        GameObject gunObj = Instantiate(prefab, transform);
        Gun gun = gunObj.GetComponent<Gun>();
        ownedGuns[gunName] = gun;

        EquipGun(gunName);
        Debug.Log("Acheté : " + gunName);
    }

    public void SwitchGun(string gunName)
    {
        if (HasGun(gunName))
            EquipGun(gunName);
        Debug.Log("Changement pour : " + gunName);
    }

    public void ReloadGun(int price)
    {
        if (money < price || currentGun == null) return;

        money -= price;
        //currentGun.Reload();
        Debug.Log("Reload de " + currentGun);
    }

    private void EquipGun(string gunName)
    {
        if (currentGun != null)
            Destroy(currentGun.gameObject);

        currentGun = Instantiate(GetGunPrefab(gunName), transform).GetComponent<Gun>();
    }

    private GameObject GetGunPrefab(string gunName)
    {
        return gunName switch
        {
            "Pistol" => pistolPrefab,
            "Rifle" => riflePrefab,
            "Shotgun" => shotgunPrefab,
            _ => null
        };
    }
}
