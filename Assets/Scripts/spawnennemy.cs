using UnityEngine;

public class spawnennemy : MonoBehaviour
{
    public GameObject enemyThief;
    public GameObject enemyArcher;
    public GameObject enemySoldier;
    public GameObject enemyPriest;
    public GameObject enemyKnight;
    private int whichCharacter;
    [SerializeField] private int spawnRate = 2;
    private float timerspawn = 0;
    private float timercheck = 0;
    [SerializeField] private static int spawnRateRange = 10;
    private int updateUnitsStateTime = 2;
    [SerializeField] private int unitscountthreshold;
    private enum TacticState
    {
        Attack,
        UltraAttack,
        Defend,
        UltraDefend
    }

    private TacticState tacticState = TacticState.Attack;

    // Start is called before the first frame update
    private void Start()
    {
        spawnEnemyArcher();
    }

    // Update is called once per frame
    private void Update()
    {
        if (timercheck < updateUnitsStateTime)
        {
            timercheck = timercheck + Time.deltaTime;
        }
        else
        {
            UpdateTacticState();
            timercheck = 0;
        }

        if (timerspawn < spawnRate)
        {
            timerspawn = timerspawn + Time.deltaTime;
        }
        else
        {
            whichCharacter = Random.Range(1, 4);
            switch (tacticState)
            {
                case TacticState.Attack:
                    if (whichCharacter == 1)
                    {
                        SpawnMele();
                    }
                    else
                    {
                        SpawnRanged();
                    }
                    spawnRateRange = 9;
                    break;

                case TacticState.Defend:
                    if (whichCharacter == 1)
                    {
                        SpawnRanged();
                    }
                    else
                    {
                        SpawnMele();
                    }
                    spawnRateRange = 9;
                    break;

                case TacticState.UltraAttack:
                    if (whichCharacter == 1)
                    {
                        SpawnMele();
                    }
                    else
                    {
                        SpawnRanged();
                    }
                    spawnRateRange = 6;
                    break;

                case TacticState.UltraDefend:
                    if (whichCharacter == 1)
                    {
                        SpawnRanged();
                    }
                    else
                    {
                        SpawnMele();
                    }
                    spawnRateRange = 6;
                    break;
            }

            timerspawn = 0;
            spawnRate = Random.Range(3, spawnRateRange);
        }
    }

    protected void SpawnMele()
    {
        int whichmele = Random.Range(1, 4);
        switch (whichmele)
        {
            case 1:
                if (spawnEnemyKnight()) { }
                else if (spawnEnemySoldier()) { }
                else { spawnEnemyThief(); }
                break;

            case 2:
                if (spawnEnemySoldier()) { }
                else { spawnEnemyThief(); }
                break;

            case 3:
                spawnEnemyThief();
                break;
        }
    }

    protected void SpawnRanged()
    {
        int whichRanged = Random.Range(1, 3);
        switch (whichRanged)
        {
            case 1:
                if (spawnEnemyPriest()) { }
                else { spawnEnemyArcher(); }
                break;

            case 2:
                spawnEnemyArcher();
                break;
        }
    }

    protected bool spawnEnemyThief()
    {
        return TrySpawnUnit(enemyThief);
    }

    protected bool spawnEnemyArcher()
    {
        return TrySpawnUnit(enemyArcher);
    }

    protected bool spawnEnemySoldier()
    {
        return TrySpawnUnit(enemySoldier);
    }

    protected bool spawnEnemyPriest()
    {
        return TrySpawnUnit(enemyPriest);
    }

    protected bool spawnEnemyKnight()
    {
        return TrySpawnUnit(enemyKnight);
    }

    private void UpdateTacticState()
    {

        GameManager.Instance.UpdateUnitsLists();
        float playerUnitsTotalDMG = GameManager.Instance.GetTotalDamage(GameManager.Instance.playerUnits);
        float enemyUnitsTotalDMG = GameManager.Instance.GetTotalDamage(GameManager.Instance.enemyUnits);
        int playerUnitsCount = GameManager.Instance.playerUnits.Count;
        int enemyUnitsCount = GameManager.Instance.enemyUnits.Count;


        ChoseTacticState(unitscountthreshold, playerUnitsTotalDMG, enemyUnitsTotalDMG, playerUnitsCount, enemyUnitsCount);



        Debug.Log(tacticState);
    }

    private bool TrySpawnUnit(GameObject unitPrefab)
    {
        Characters characterScript = unitPrefab.GetComponent<Characters>();
        if (characterScript != null)
        {
            int unitCost = characterScript.GetPrice();
            int unitExpThreshold = characterScript.GetExpNeeded();

            if (GameManager.Instance.BuyUnit(unitCost, unitExpThreshold, Characters.Faction.Enemy))
            {
                Instantiate(unitPrefab, transform.position, transform.rotation);
                return true;
            }
            else return false;
        }
        else
        {
            Debug.LogError("Brak skryptu Characters na jednostce!");
            return false;
        }
    }
    private void ChoseTacticState(int unitscountthreshold, float playerunitstotalDMG, float enemyunitstotalDMG ,int playerunitscount,int enemyunitscount)
    {
        if (playerunitstotalDMG > enemyunitstotalDMG)
        {
            if (enemyunitscount< unitscountthreshold) { tacticState = TacticState.UltraDefend; }
            else tacticState = TacticState.Defend;
        }
        else
        {
            if (playerunitscount< unitscountthreshold) { tacticState = TacticState.UltraAttack; }
            else tacticState = TacticState.Attack;
        }
    }
}