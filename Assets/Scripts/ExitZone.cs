using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Un ennemi est entré dans la zone de sortie !");
            GameManager._Instance.ShowGameOver(); // Appelle directement le GameManager
        }
    }
}
