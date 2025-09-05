using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private int _gunIndex;

    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(AttemptPurchase);

        ResourceManager._InstanceResource.OnResourceChanged += CheckResources;
        StartCoroutine(DelayedCheckResources());
    }

    private IEnumerator DelayedCheckResources()
    {
        yield return null;
        CheckResources();
    }

    private void OnDestroy()
    {
        if (ResourceManager._InstanceResource != null)
            ResourceManager._InstanceResource.OnResourceChanged -= CheckResources;

        _button.onClick.RemoveListener(AttemptPurchase);
    }

    private void CheckResources()
    {
        var gunManager = GunManager._InstanceGunManager;
        var resourceManager = ResourceManager._InstanceResource;

        if (gunManager == null || resourceManager == null)
            return;

        var gun = gunManager._allGuns[_gunIndex];

        if (gunManager.GunsOwned[_gunIndex])
        {
            _button.interactable = true;
            return;
        }

        int current = resourceManager.GetCurrentResource();
        _button.interactable = current >= gun._price;
    }

    private void AttemptPurchase()
    {
        if (_clickClip != null)
            AudioSource.PlayClipAtPoint(_clickClip, transform.position);

        GunManager._InstanceGunManager.TryBuyAndEquipGun(_gunIndex);
        CheckResources();
    }
}
