using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private int _itemCost = 10;
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();


        _button.onClick.AddListener(AttemptPurchase);


        ResourceManager._Instance.OnResourceChanged += CheckResources;

        CheckResources();
    }

    private void OnDestroy()
    {
        if (ResourceManager._Instance != null)
            ResourceManager._Instance.OnResourceChanged -= CheckResources;

        _button.onClick.RemoveListener(AttemptPurchase);
    }

    private void CheckResources()
    {
        int current = ResourceManager._Instance.GetCurrentResource();
        _button.interactable = current >= _itemCost;
    }

    private void AttemptPurchase()
    {
        bool success = ResourceManager._Instance.SpendResource(_itemCost);

        if (success)
        {
            Debug.Log("Achat réussi pour " + _itemCost + " ressources !");
        }
        else
        {
            Debug.Log("Pas assez de ressources !");
        }
    }
}