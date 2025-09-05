using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager _InstanceResource;

    public event Action OnResourceChanged;

    [SerializeField] private TextMeshProUGUI _resourceText;

    private int _resources = 0;

    private void Awake()
    {
        if (_InstanceResource != null)
        {
            Destroy(gameObject);
            return;
        }
        _InstanceResource = this;
    }

    private void Start()
    {
        UpdateUI();
    }


    public void AddResource(int amount)
    {
        _resources += amount;
        Debug.Log($"Ajouté {amount} ressources. Total : {_resources}");
        UpdateUI();
        OnResourceChanged?.Invoke();
    }

    public bool SpendResource(int amount)
    {
        if (_resources >= amount)
        {
            _resources -= amount;
            Debug.Log($"Dépensé {amount} ressources. Reste : {_resources}");
            UpdateUI();
            OnResourceChanged?.Invoke();
            return true;
        }
        Debug.LogWarning("Pas assez de ressources !");
        return false;
    }

    public int GetCurrentResource() => _resources;

    private void UpdateUI()
    {
        if (_resourceText != null)
            _resourceText.text = "Ressources : " + _resources;
    }
}