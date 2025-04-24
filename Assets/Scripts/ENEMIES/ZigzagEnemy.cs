using UnityEngine;

public class ZigzagEnemy : ParentEnemy
{
    [SerializeField] private float zigzagFrequency = 3f;   // Fréquence modérée pour un zigzag lisible
    [SerializeField] private float zigzagAmplitude = 2.5f; // Amplitude raisonnable pour ne pas sortir trop du chemin
    [SerializeField] private float _movementSpeed = 6f;    // Vitesse fluide sans rendre l'esquive impossible

    protected override void Move()
    {
        Vector3 targetPos;

        if (EnemyManager.Instance.ShouldAttackPlayer(this))
        {
            targetPos = hero.position;
        }
        else
        {
            targetPos = EnemyManager.Instance.GetExit().position;
        }

        // Direction vers la cible
        Vector3 forward = (targetPos - transform.position).normalized;

        // Oscillation latérale sur l'axe perpendiculaire (droite/gauche)
        Vector3 right = Vector3.Cross(Vector3.up, forward);
        float oscillation = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;

        // Position avec décalage zigzag
        Vector3 zigzagTarget = transform.position + forward * _movementSpeed * Time.deltaTime + right * oscillation * Time.deltaTime;

        // On avance directement vers ce point
        transform.position = zigzagTarget;

        // Orientation vers la cible pour l’animation
        transform.forward = forward;

        animationLinker.Walk();
    }

    protected override System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animationLinker.Attack();

        yield return new WaitForSeconds(0.5f);

        if (Vector3.Distance(transform.position, hero.position) <= attackRange)
        {
            //HeroHealth._Instance.TakeDamage(damage);
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }
}
