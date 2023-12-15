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
        // Wykonaj raycast, aby sprawdzi� obiekty w zasi�gu ataku
        RaycastHit2D hit = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemy);

        if (hit.collider != null)
        {
            // Sprawd�, czy trafiony obiekt posiada komponent "Health"
            Health targetHealth = hit.collider.GetComponent<UnitHealth>();

            if (targetHealth != null)
            {
                audioMenager.PlaySFX(audioMenager.swoosh);
                // Sprawd� odleg�o�� mi�dzy jednostkami
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);

                if (distance <= rangeMultiplayer / attackRange)
                {
                    // Atakuj z wi�kszym obra�eniem
                    targetHealth.TakeDamage(damage);
                }
                else
                {
                    // Atakuj standardowym obra�eniem
                    targetHealth.TakeDamage(damage/dmgMultiplayer);
                }
            }
        }
    }

    // Nadpisz metod� AttackBase
    protected override void AttackBase()
    {
        // Wykonaj raycast, aby sprawdzi� obiekty w zasi�gu ataku
        RaycastHit2D hit = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemyBase);

        if (hit.collider != null)
        {
            // Sprawd�, czy trafiony obiekt posiada komponent "Health"
            Health targetHealth = hit.collider.GetComponent<BaseHealth>();

            if (targetHealth != null)
            {
                audioMenager.PlaySFX(audioMenager.swoosh);
                // Sprawd� odleg�o�� mi�dzy jednostkami
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);

                if (distance - 1.5 <= rangeMultiplayer / attackRange)
                {
                    // Atakuj z wi�kszym obra�eniem
                    targetHealth.TakeDamage(damage);
                }
                else
                {
                    // Atakuj standardowym obra�eniem
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