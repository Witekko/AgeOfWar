using UnityEngine;

public class Player : MonoBehaviour
{
    // Dodaj referencjê do GameManager, jeœli jeszcze nie jest przypisana w inspektorze
    [SerializeField] protected GameManager gameManager;

    [SerializeField] protected int gold = 0;
    [SerializeField] protected int exp = 0;

    // Pozosta³a czêœæ kodu...

    protected virtual void Awake()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
    }

    public int Exp
    {
        get { return exp; }
    }
    public virtual void AddGold(int amount)
    {
        gold += amount;
    }

    public virtual void AddExp(int amount)
    {
        exp += amount;
    }

    public virtual bool HasEnoughGold(int amount)
    {
        return gold >= amount;
    }

    public virtual bool HasEnoughExp(int amount)
    {
        return exp >= amount;
    }

    public virtual bool Buy(int cost, int exphreshold)
    {
        if (HasEnoughGold(cost) && HasEnoughExp(exphreshold))
        {
            AddGold(-cost);
            return true;
        }
        else
        {
            return false;
        }
    }
}