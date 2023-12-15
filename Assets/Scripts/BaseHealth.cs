using UnityEngine;

public class BaseHealth : Health
{
    // Start is called before the first frame update

    [SerializeField] protected SceneMenager sceneMenager;
    [SerializeField] private Characters.Faction faction;

    public override void Awake()
    {
        base.Awake();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (faction == Characters.Faction.Enemy)
        {
            sceneMenager.UpdateUIEnemyHP(currentHealth);
        }
        else if (faction == Characters.Faction.Ally)
        {
            sceneMenager.UpdateUIPlayerHP(currentHealth);
        }
    }

    public override void Die()
    {
        GameManager.Instance.EndGame(faction);
    }
}