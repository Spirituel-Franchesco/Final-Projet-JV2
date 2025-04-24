using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private int gold = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        //Debug.Log("💰 Or total : " + gold);
        Debug.Log("Or total : " + gold);
        // mettre à jour UI si besoin
    }

    public int GetGold() => gold;
}
