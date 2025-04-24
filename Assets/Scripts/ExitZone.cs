using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public int enemiesPassed = 0;
    public TMPro.TextMeshProUGUI counterText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesPassed++;
            counterText.text = "Ennemis échappés : " + enemiesPassed;
            Destroy(other.gameObject); // ou autre comportement
        }
    }
}

