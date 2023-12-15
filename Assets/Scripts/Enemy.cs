public class Enemy : Player
{
    public override void AddExp(int amount)
    {
        base.AddExp(amount);
    }

    public override void AddGold(int amount)
    {
        base.AddGold(amount);
    }

    public override bool Buy(int cost, int exphreshold)
    {
        return base.Buy(cost, exphreshold);
    }

    public override bool HasEnoughExp(int amount)
    {
        return base.HasEnoughExp(amount);
    }

    public override bool HasEnoughGold(int amount)
    {
        return base.HasEnoughGold(amount);
    }

    protected override void Awake()
    {
        base.Awake();
    }
}