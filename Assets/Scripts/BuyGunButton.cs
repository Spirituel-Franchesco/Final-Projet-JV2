using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyGunButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    [TextArea]
    public string _tooltipContent;

    public void OnPointerEnter(PointerEventData eventData)

    {
        Debug.Log("SURVOL détecté");
        ToolTipSystem._Instance.Show(_tooltipContent, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem._Instance.Hide();
    }
}
