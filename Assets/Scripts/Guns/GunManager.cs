using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{
    public static GunManager _InstanceGunManager;

    public TextMeshProUGUI _ammoText;
    public TextMeshProUGUI _gunNameText; // ← Optionnel pour afficher le nom du gun
    public Gun[] _allGuns;
    public bool[] GunsOwned => _gunsOwned; // Propriété pour accéder à la liste des armes possédées

    public int _money = 100;

    private bool[] _gunsOwned;
    private Gun _currentGun;

    private void Awake()
    {
        if (_InstanceGunManager == null) _InstanceGunManager = this;
    }

    private void Start()
    {
        _gunsOwned = new bool[_allGuns.Length];
        _gunsOwned[0] = true; // PA Gun gratuit
        EquipGun(0);
    }
    
    private void Update()
    {
        if (_currentGun != null && Input.GetButtonDown("Fire1"))
        {
            _currentGun.Shoot();
            UpdateAmmoUI();
        }

        if (_currentGun != null && Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentGun();
        }
    }

    public void EquipGun(int index)
    {
        for (int i = 0; i < _allGuns.Length; i++)
        {
            _allGuns[i].gameObject.SetActive(i == index);
        }

        _currentGun = _allGuns[index];
        UpdateAmmoUI();
        UpdateGunNameUI();
    }

    public void TryBuyAndEquipGun(int index)
    {
        if (_gunsOwned[index])
        {
            EquipGun(index); // déjà acheté → juste équiper
            Debug.Log($"Arme {index + 1} équipée.");
            return;
        }

        Gun gunToBuy = _allGuns[index];

        if (ResourceManager._InstanceResource.SpendResource(gunToBuy._price))
        {
            _gunsOwned[index] = true; // on marque comme achetée → reste permanent
            EquipGun(index);
            Debug.Log($"Arme {index + 1} achetée et équipée !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour acheter cette arme !");
        }
    }

    public void ReloadCurrentGun()
    {
        if (_currentGun != null && !_currentGun._isReloading)
        {
            _currentGun.StartReload();
            UpdateAmmoUI();
        }
    }

    private void UpdateAmmoUI()
    {
        if (_currentGun != null && _ammoText != null)
        {
            _ammoText.text = $"Ammo: {_currentGun._currentAmmo} / {_currentGun._maxAmmo}";
        }
    }

    private void UpdateGunNameUI()
    {
        if (_currentGun != null && _gunNameText != null)
        {
            _gunNameText.text = _currentGun.gameObject.name;
        }
    }
}
