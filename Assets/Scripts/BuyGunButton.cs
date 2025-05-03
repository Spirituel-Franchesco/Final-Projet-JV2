using UnityEngine;
using UnityEngine.EventSystems;

public class BuyGunButton : MonoBehaviour
{
    [TextArea]
    public string tooltipContent;

    public void OnPointerEnter(PointerEventData eventData)

    {
        Debug.Log("SURVOL détecté");
        ToolTipSystem.Instance.Show(tooltipContent, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.Instance.Hide();
    }
}
