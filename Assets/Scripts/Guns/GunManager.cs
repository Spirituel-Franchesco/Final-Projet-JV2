using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;

    public Gun[] allGuns;
    private Gun currentGun;

    public int money = 100;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI gunNameText; // ← Optionnel pour afficher le nom du gun

    private bool[] gunsOwned;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        //gunsOwned = new bool[allGuns.Length];
        //gunsOwned[0] = true; // Le premier fusil est toujours possédé
        //EquipGun(0);

        gunsOwned = new bool[allGuns.Length];
        for (int i = 0; i < gunsOwned.Length; i++)
            gunsOwned[i] = true;  // ← toutes les armes sont considérées achetées

        EquipGun(0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            currentGun?.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //TryReload();
        }

        // Changement d'arme : touches 1, 2, 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (gunsOwned[0]) EquipGun(0);
            else Debug.Log("Arme 1 non achetée");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (gunsOwned.Length > 1 && gunsOwned[1]) EquipGun(1);
            else Debug.Log("Arme 2 non achetée");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (gunsOwned.Length > 2 && gunsOwned[2]) EquipGun(2);
            else Debug.Log("Arme 3 non achetée");
        }

        UpdateAmmoUI();
    }


    public void EquipGun(int index)
    {
        for (int i = 0; i < allGuns.Length; i++)
            allGuns[i].gameObject.SetActive(i == index);

        currentGun = allGuns[index];
        UpdateAmmoUI();
        UpdateGunNameUI();
    }

    //public void BuyGun(int index)
    //{
    //    Gun gunToBuy = allGuns[index];
    //    if (gunsOwned[index])
    //    {
    //        EquipGun(index);
    //        Debug.Log("Arme déjà achetée, juste équipée.");
    //    }
    //    else if (money >= gunToBuy.price)
    //    {
    //        money -= gunToBuy.price;
    //        gunsOwned[index] = true;
    //        EquipGun(index);
    //        Debug.Log($"Arme {index + 1} achetée et équipée.");
    //    }
    //    else
    //    {
    //        Debug.Log("Pas assez d'argent pour acheter cette arme.");
    //    }
    //}

    public void BuyGun(int index)
    {
        Gun gunToBuy = allGuns[index];

        // TEMPORAIRE : on force l'arme comme "achetée" sans retirer de l'argent
        gunsOwned[index] = true;
        EquipGun(index);
        Debug.Log($"[TEST] Arme {index + 1} débloquée et équipée.");

        // Si tu veux revenir au système normal, remets ce bloc à la place :
        /*
        if (gunsOwned[index])
        {
            EquipGun(index);
            Debug.Log("Arme déjà achetée, juste équipée.");
        }
        else if (money >= gunToBuy.price)
        {
            money -= gunToBuy.price;
            gunsOwned[index] = true;
            EquipGun(index);
            Debug.Log($"Arme {index + 1} achetée et équipée.");
        }
        else
        {
            Debug.Log("Pas assez d'argent pour acheter cette arme.");
        }
        */
    }


    //public void TryReload()
    //{
    //    if (money >= 10)
    //    {
    //        money -= 10;
    //        currentGun?.Reload();
    //    }
    //    else
    //    {
    //        Debug.Log("Pas assez d'argent pour recharger");
    //    }
    //}

    private void UpdateAmmoUI()
    {
        if (currentGun != null && ammoText != null)
        {
            ammoText.text = $"Ammo: {currentGun.currentAmmo} / {currentGun.maxAmmo}";
        }
    }

    private void UpdateGunNameUI()
    {
        if (currentGun != null && gunNameText != null)
        {
            gunNameText.text = currentGun.gameObject.name;
        }
    }
}
