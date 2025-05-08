using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{
    public static GunManager _Instance;

    public TextMeshProUGUI _ammoText;
    public TextMeshProUGUI _gunNameText; // ← Optionnel pour afficher le nom du gun
    public Gun[] _allGuns;
    public int _money = 100;

    private bool[] _gunsOwned;
    private Gun _currentGun;

    private void Awake()
    {
        if (_Instance == null) _Instance = this;
    }

    private void Start()
    {
        _gunsOwned = new bool[_allGuns.Length];
        for (int i = 0; i < _gunsOwned.Length; i++)
            _gunsOwned[i] = true;  // ← toutes les armes sont considérées achetées

        EquipGun(0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            _currentGun?.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //TryReload();
        }

        // Changement d'arme : touches 1, 2, 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_gunsOwned[0]) EquipGun(0);
            else Debug.Log("Arme 1 non achetée");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_gunsOwned.Length > 1 && _gunsOwned[1]) EquipGun(1);
            else Debug.Log("Arme 2 non achetée");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_gunsOwned.Length > 2 && _gunsOwned[2]) EquipGun(2);
            else Debug.Log("Arme 3 non achetée");
        }

        UpdateAmmoUI();
    }


    public void EquipGun(int index)
    {
        for (int i = 0; i < _allGuns.Length; i++)
            _allGuns[i].gameObject.SetActive(i == index);

        _currentGun = _allGuns[index];
        UpdateAmmoUI();
        UpdateGunNameUI();
    }

    public void BuyGun(int index)
    {
        Gun gunToBuy = _allGuns[index];

        // TEMPORAIRE : on force l'arme comme "achetée" sans retirer de l'argent
        _gunsOwned[index] = true;
        EquipGun(index);
        Debug.Log($"[TEST] Arme {index + 1} débloquée et équipée.");
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
