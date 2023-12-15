using UnityEngine;

public class soldierAction : Characters
{
    [SerializeField] private Animator anim;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        AnimatorController();
    }

    protected override void Attack()
    {
        base.Attack();
        audioMenager.PlaySFX(audioMenager.sword);
    }

    protected override void AttackBase()
    {
        base.AttackBase();
        audioMenager.PlaySFX(audioMenager.sword);
    }

    protected virtual void AnimatorController()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isWinner", isWinner);
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
        anim.SetTrigger("isDyingTrigger");
        //tutaj kwestie zwiazane z animacja
        //tutaj destroy
    }
}