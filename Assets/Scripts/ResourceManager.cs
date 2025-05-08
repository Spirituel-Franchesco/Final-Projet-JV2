using UnityEngine;
using TMPro;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager _Instance;

    private int _resources = 0;

    [SerializeField] private TextMeshProUGUI _resourceText;


    public event Action OnResourceChanged;

    void Awake()
    {
        if (_Instance != null)
        {
            Debug.LogError("Il y a déjà un ResourceManager dans la scène !");
            return;
        }

        _Instance = this;
    }


    public void AddResource(int amount)
    {
        _resources += amount;
        UpdateUI();
        OnResourceChanged?.Invoke();
    }

    public bool SpendResource(int amount)
    {
        if (_resources >= amount)
        {
            _resources -= amount;
            UpdateUI();
            OnResourceChanged?.Invoke();
            return true;
        }

        return false;
    }

    public int GetCurrentResource() => _resources;

    private void UpdateUI()
    {
        if (_resourceText != null)
            _resourceText.text = "Ressources : " + _resources;
    }



}