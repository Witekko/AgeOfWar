using UnityEngine;

public class archerAction : Characters
{
    public Animator anim;
    [SerializeField] private float rangeMultiplayer;
    [SerializeField] private float dmgMultiplayer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        AnimatorController();
    }

    protected virtual void AnimatorController()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isWinner", isWinner);
    }

    protected override void Attack()
    {
        // Wykonaj raycast, aby sprawdziæ obiekty w zasiêgu ataku
        RaycastHit2D hit = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemy);

        if (hit.collider != null)
        {
            // SprawdŸ, czy trafiony obiekt posiada komponent "Health"
            Health targetHealth = hit.collider.GetComponent<UnitHealth>();

            if (targetHealth != null)
            {
                audioMenager.PlaySFX(audioMenager.swoosh);
                // SprawdŸ odleg³oœæ miêdzy jednostkami
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);

                if (distance <= rangeMultiplayer / attackRange)
                {
                    // Atakuj z wiêkszym obra¿eniem
                    targetHealth.TakeDamage(damage);
                }
                else
                {
                    // Atakuj standardowym obra¿eniem
                    targetHealth.TakeDamage(damage/dmgMultiplayer);
                }
            }
        }
    }

    // Nadpisz metodê AttackBase
    protected override void AttackBase()
    {
        // Wykonaj raycast, aby sprawdziæ obiekty w zasiêgu ataku
        RaycastHit2D hit = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemyBase);

        if (hit.collider != null)
        {
            // SprawdŸ, czy trafiony obiekt posiada komponent "Health"
            Health targetHealth = hit.collider.GetComponent<BaseHealth>();

            if (targetHealth != null)
            {
                audioMenager.PlaySFX(audioMenager.swoosh);
                // SprawdŸ odleg³oœæ miêdzy jednostkami
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);

                if (distance - 1.5 <= rangeMultiplayer / attackRange)
                {
                    // Atakuj z wiêkszym obra¿eniem
                    targetHealth.TakeDamage(damage);
                }
                else
                {
                    // Atakuj standardowym obra¿eniem
                    targetHealth.TakeDamage(damage/dmgMultiplayer);
                }
            }
        }
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
        anim.SetTrigger("isDyingTrigger");
        //tutaj kwestie zwiazane z animacja
        //tutaj destroy
    }
}