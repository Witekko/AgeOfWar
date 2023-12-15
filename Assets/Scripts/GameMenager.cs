using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private SceneMenager sceneMenager;

    public MePlayer player;
    public Enemy enemy;
    private bool isGameFinished = false;
    public List<Characters> playerUnits = new List<Characters>();
    public List<Characters> enemyUnits = new List<Characters>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    }

    public void AddGold(int amount, Characters.Faction faction)
    {
        switch (faction)
        {
            case Characters.Faction.Enemy:
                player.AddGold(amount);
                break;

            case Characters.Faction.Ally:
                enemy.AddGold(amount);
                break;
        }
    }

    public void AddExp(int amount, Characters.Faction faction)
    {
        switch (faction)
        {
            case Characters.Faction.Enemy:
                player.AddExp(amount);
                break;

            case Characters.Faction.Ally:
                enemy.AddExp(amount);
                break;
        }
    }

    public bool BuyUnit(int cost, int expthreshold, Characters.Faction faction)
    {
        if (!isGameFinished)
        {
            if (faction == Characters.Faction.Ally) { return player.Buy(cost, expthreshold); }
            if (faction == Characters.Faction.Enemy) { return enemy.Buy(cost, expthreshold); }
        }
        return false;
    }

    public void EndGame(Characters.Faction faction)
    {
        isGameFinished = true;
        if (faction == Characters.Faction.Enemy)
        {
            sceneMenager.SetSceneActive(Winner.Player);
            SetWinnerCharacters(Characters.Faction.Ally); // Ustaw zwyciêzcom flagê isWinner
        }
        else if (faction == Characters.Faction.Ally)
        {
            sceneMenager.SetSceneActive(Winner.Enemy);
            SetWinnerCharacters(Characters.Faction.Enemy); // Ustaw zwyciêzcom flagê isWinner
        }
    }

    public void SetWinnerCharacters(Characters.Faction winningFaction)
    {
        Characters[] characters = FindObjectsOfType<Characters>();

        foreach (Characters character in characters)
        {
            // Sprawdzamy bezpoœrednio pole faction w skrypcie Characters
            if (character.faction == winningFaction)
            {
                character.SetWinnerFlag();
            }
        }
    }

    public void UpdateUnitsLists()
    {
        playerUnits.Clear();
        enemyUnits.Clear();
        Characters[] characters = FindObjectsOfType<Characters>();

        foreach (Characters character in characters)
        {
            if (character.faction == Characters.Faction.Ally)
            {
                playerUnits.Add(character);
            }
            else if (character.faction == Characters.Faction.Enemy)
            {
                enemyUnits.Add(character);
            }
        }
    }

    public float GetTotalDamage(List<Characters> units)
    {
        float totalDamage = 0;

        foreach (Characters unit in units)
        {
            float unitDamage = unit.Damage;
            totalDamage += unitDamage;
        }

        return totalDamage;
    }

    public void restartGame()
    {
        // Dodaj wywo³anie SetGameScreen() po restarcie gry
        sceneMenager.SetGameScreen();
    }
}