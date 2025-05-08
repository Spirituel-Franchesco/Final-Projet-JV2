using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public static ToolTipSystem _Instance;

    [SerializeField] private GameObject _tooltipPanel;
    [SerializeField] private TextMeshProUGUI _tooltipText;

    void Awake()
    {
        _Instance = this;
        Hide();
    }

    public void Show(string content, Vector2 position)
    {
        Debug.Log("TOOLTIP SHOW: " + content); // ← AJOUTE CETTE LIGNE
        _tooltipPanel.SetActive(true);
        _tooltipText.text = content;
        _tooltipPanel.transform.position = position;
    }


    public void Hide()
    {
        _tooltipPanel.SetActive(false);


    }
}
