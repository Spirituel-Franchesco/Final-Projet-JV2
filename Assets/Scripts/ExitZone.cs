using UnityEngine;

public class ExitZone : MonoBehaviour
{
    [SerializeField] private AudioClip _exitClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_exitClip != null)
                AudioSource.PlayClipAtPoint(_exitClip, transform.position);

            Debug.Log("Un ennemi est entré dans la zone de sortie !");
            GameManager._Instance.ShowGameOver(); // Appelle directement le GameManager
        }
    }
}
