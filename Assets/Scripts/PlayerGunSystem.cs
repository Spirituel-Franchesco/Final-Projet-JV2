using System.Collections.Generic;
using UnityEngine;

public class PlayerGunSystem : MonoBehaviour
{
    public GameObject _pistolPrefab;
    public GameObject _riflePrefab;
    public GameObject _shotgunPrefab;
    public Transform _shootOrigin;

    public int _money = 200;

    private Dictionary<string, Gun> _ownedGuns = new();
    private Gun _currentGun;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentGun != null) ;
               //_currentGun.Shoot(_shootOrigin);
        }
    }

    public bool HasGun(string gunName)
    {
        return _ownedGuns.ContainsKey(gunName);
    }

    public void BuyGun(string gunName, int price)
    {
        if (_money < price) return;

        _money -= price;
        GameObject prefab = GetGunPrefab(gunName);
        if (prefab == null) return;

        GameObject gunObj = Instantiate(prefab, transform);
        Gun gun = gunObj.GetComponent<Gun>();
        _ownedGuns[gunName] = gun;

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
        if (_money < price || _currentGun == null) return;

        _money -= price;
        //_currentGun.Reload();
        Debug.Log("Reload de " + _currentGun);
    }

    private void EquipGun(string gunName)
    {
        if (_currentGun != null)
            Destroy(_currentGun.gameObject);

        _currentGun = Instantiate(GetGunPrefab(gunName), transform).GetComponent<Gun>();
    }

    private GameObject GetGunPrefab(string gunName)
    {
        return gunName switch
        {
            "Pistol" => _pistolPrefab,
            "Rifle" => _riflePrefab,
            "Shotgun" => _shotgunPrefab,
            _ => null
        };
    }
}
