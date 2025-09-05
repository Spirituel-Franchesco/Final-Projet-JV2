using UnityEngine;

public class TestDebug : MonoBehaviour

{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ParentEnemy[] enemies =  FindObjectsOfType<ParentEnemy>();

            if (enemies.Length == 0)
            {
                Debug.Log("Aucun ennemi à tuer.");
                return;
            }

            foreach (var ParentEnemy in enemies)
            {
                ParentEnemy.TakeDamage(9999);
            }

            Debug.Log("Touche K pressée – Tous les ennemis éliminés !");
        }
    }
}

