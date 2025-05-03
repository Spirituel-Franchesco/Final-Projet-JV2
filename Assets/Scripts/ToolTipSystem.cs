using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public static ToolTipSystem Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(string content, Vector2 position)
    {
        Debug.Log("TOOLTIP SHOW: " + content); // ← AJOUTE CETTE LIGNE
        tooltipPanel.SetActive(true);
        tooltipText.text = content;
        tooltipPanel.transform.position = position;
    }


    public void Hide()
    {
        tooltipPanel.SetActive(false);


    }
}
