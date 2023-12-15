public class UnitHealth : Health
{
    // Start is called before the first frame update

    public int goldReward;
    public int expReward;

    public event System.Action OnDeath;

    private Characters.Faction unitFaction;

    public override void Awake()
    {
        base.Awake();
    }

    public void Initialize(Characters.Faction faction)
    {
        unitFaction = faction;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        GiveRewards(goldReward, expReward);
        if (OnDeath != null)
        {
            OnDeath.Invoke();
        }
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    protected void GiveRewards(int gold, int exp)
    {
        GameManager.Instance.AddGold(gold, unitFaction);
        GameManager.Instance.AddExp(exp, unitFaction);
    }
}