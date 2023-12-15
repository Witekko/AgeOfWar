using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject thief;
    public GameObject archer;
    public GameObject soldier;
    public GameObject priest;
    public GameObject knight;

    [SerializeField] private Text unitInfoText;

    // Start is called before the first frame update
    private MePlayer player;

    private void Start()
    {
        player = FindObjectOfType<MePlayer>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SpawnThieff()
    {
        TrySpawnUnit(thief);
    }

    public void SpawnArcher()
    {
        TrySpawnUnit(archer);
    }

    public void SpawnSoldier()
    {
        TrySpawnUnit(soldier);
    }

    public void SpawnPriest()
    {
        TrySpawnUnit(priest);
    }

    public void SpawnKnight()
    {
        TrySpawnUnit(knight);
    }

    private void TrySpawnUnit(GameObject unitPrefab)
    {
        Characters characterScript = unitPrefab.GetComponent<Characters>();
        if (characterScript != null)
        {
            int unitCost = characterScript.GetPrice();
            int unitExpThreshold = characterScript.GetExpNeeded();

            if (GameManager.Instance.BuyUnit(unitCost, unitExpThreshold, Characters.Faction.Ally))
            {
                Instantiate(unitPrefab, transform.position, transform.rotation);
            }
        }
        else
        {
            Debug.LogError("Brak skryptu Characters na jednostce!");
        }
    }

    // Funkcja do wyœwietlania informacji o jednostce w HUDzie

    public void OnPointerEnterThieff()
    {
        OnPointerEnter(thief);
    }

    public void OnPointerEnterArcher()
    {
        OnPointerEnter(archer);
    }

    public void OnPointerEnterSoldier()
    {
        OnPointerEnter(soldier);
    }

    public void OnPointerEnterPriest()
    {
        OnPointerEnter(priest);
    }

    public void OnPointerEnterKnight()
    {
        OnPointerEnter(knight);
    }

    public void OnPointerExit()
    {
        if (unitInfoText != null)
        {
            ClearUnitInfo();
        }
    }

    private void OnPointerEnter(GameObject unitPrefab)
    {
        string unitInfo = GetUnitInfo(unitPrefab);
        ShowUnitInfo(unitInfo);
    }

    public string GetUnitInfo(GameObject unitPrefab)
    {
        Characters characterScript = unitPrefab.GetComponent<Characters>();
        int unitCost = characterScript.GetPrice();
        int unitExpThreshold = characterScript.GetExpNeeded();
        return $"Price: Gold - {unitCost}, \nExp. Required: - {unitExpThreshold}";
    }

    public void ShowUnitInfo(string info)
    {
        unitInfoText.text = "Info: " + info;
        Invoke("ClearUnitInfo", 3f); // Wywo³aj ClearUnitInfo po 3 sekundach
    }

    public void ClearUnitInfo()
    {
        unitInfoText.text = "";
    }
}