using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunUIManager : MonoBehaviour
{
    public Button rifleButton;
    public Button shotgunButton;
    public Button reloadButton;

    private PlayerGunSystem player; // on va le connecter à notre joueur
    private int riflePrice = 100;
    private int shotgunPrice = 150;
    private int reloadPrice = 50;

    void Start()
    {
        player = FindObjectOfType<PlayerGunSystem>();

        rifleButton.onClick.AddListener(() => TryBuyOrSwitchGun("Rifle", riflePrice));
        shotgunButton.onClick.AddListener(() => TryBuyOrSwitchGun("Shotgun", shotgunPrice));
        reloadButton.onClick.AddListener(() => TryReload());
    }

    void TryBuyOrSwitchGun(string gunName, int price)
    {
        if (player.HasGun(gunName))
        {
            player.SwitchGun(gunName);
        }
        else if (player.money >= price)
        {
            player.BuyGun(gunName, price);
        }
    }

    void TryReload()
    {
        if (player.money >= reloadPrice)
        {
            player.ReloadGun(reloadPrice);
        }
    }
}
