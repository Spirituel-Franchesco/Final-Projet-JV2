using UnityEngine;

public class SniperRifle : Gun
{
    public float range = 200f; // Portée élevée
    public float criticalChance = 0.2f; // 20% de chance de coup critique
    public float criticalMultiplier = 2f; // Dégâts doublés en cas de critique
    public Camera playerCamera;

    protected override void HandleShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                //float finalDamage = damage;
                //if (Random.value <= criticalChance) // Coup critique
                //{
                //    finalDamage *= criticalMultiplier;
                //    Debug.Log("Coup critique !");
                //}
                //enemy.TakeDamage(finalDamage);
            }
        }
    }
}