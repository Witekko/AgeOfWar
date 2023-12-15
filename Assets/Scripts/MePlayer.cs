using UnityEngine;

public class MePlayer : Player
{
    [SerializeField] protected SceneMenager sceneMenager;

    // Start is called before the first frame update
    public override void AddExp(int amount)
    {
        base.AddExp(amount);
        sceneMenager.UpdateUIExp(exp);
    }

    public override void AddGold(int amount)
    {
        base.AddGold(amount);
        sceneMenager.UpdateUIGold(gold);
    }

    public override bool Buy(int cost, int exphreshold)
    {
        sceneMenager.UpdateUIGold(gold);
        return base.Buy(cost, exphreshold);
    }

    public override bool HasEnoughGold(int amount)
    {
        return base.HasEnoughGold(amount);
    }

    protected virtual void Start()
    {
        if (sceneMenager != null)
        {
            sceneMenager.UpdateUIGold(gold);
            sceneMenager.UpdateUIExp(exp);
        }
        else
        {
            Debug.LogError("sceneMenager is not assigned in MePlayer.");
        }
    }
}