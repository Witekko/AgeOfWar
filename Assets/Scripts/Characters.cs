using UnityEngine;

public class Characters : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float moveSpeed;

    [SerializeField] protected float checkDistance;
    [SerializeField] protected float attackRange;
    [SerializeField] protected LayerMask whatIsEnemyBase;
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected LayerMask whatIsEnemyUnit;
    [SerializeField] protected LayerMask whatIsCharacter;
    [SerializeField] protected Transform attackRangeTransform;
    [SerializeField] protected Transform checkDistanceTransform;
    protected bool isCharacterDetected;
    protected bool isEnemyDetected;
    protected bool isEnemyUnitDetected;
    protected bool isEnemyBaseDetected;
    protected bool isMoving;
    protected bool isWinner = false;
    protected bool isDead = false;
    protected bool isAttacking;
    protected bool hasStartedAttacking;
    [SerializeField] protected int facingDirection;
    [SerializeField] protected float damage; // Iloœæ obra¿eñ zadawanych przez jednostkê
    [SerializeField] protected float attackCooldown; // Czas oczekiwania miêdzy atakami
    private float startTime;
    [SerializeField] protected int price;
    [SerializeField] protected int expThreshold;
    private UnitHealth unitHealth;
    protected AudioMenager audioMenager;

    public enum Faction
    {
        Ally,
        Enemy
    }

    [SerializeField] public Faction faction;

    protected virtual void Start()
    {
        Debug.Log("start");

        unitHealth = GetComponent<UnitHealth>();
        unitHealth.Initialize(faction); // ustawienie informacji o frakcji w UnitHealth
        audioMenager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioMenager>();
        // Zasubskrybuj metodê HandleDeath() do zdarzenia œmierci
        unitHealth.OnDeath += HandleDeath;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        enemyCheck();
        CharacterDistanceCheck();
        baseCheck();
        enemyUnitcheck();
        if (isEnemyUnitDetected && !isDead && !isWinner)
        {
            if (!hasStartedAttacking)
            {
                // Jeœli to jest pierwszy atak, ustaw flagê hasStartedAttacking na true i zainicjuj startTime
                hasStartedAttacking = true;
                startTime = Time.time;
            }

            isAttacking = true;

            if (Time.time - startTime >= attackCooldown)
            {
                if (isEnemyDetected)
                {
                    Attack();
                }
                else if (isEnemyBaseDetected)
                {
                    AttackBase();
                }

                startTime = Time.time; // Resetuj startTime po wykonaniu ataku
                isAttacking = false; // Po wykonaniu ataku resetuj flagê
            }
        }
        else
        {
            hasStartedAttacking = false; // Zresetuj flagê, gdy nie ma wrogów w zasiêgu
            isAttacking = false;
        }

        if (isCharacterDetected || isWinner)
        {
            StopCharacter();
        }
        else
        {
            MoveCharacter();
        }
    }

    protected void OnDrawGizmos()
    {
        // Raycast do wykrywania sojuszników i zatrzymania postaci
        Gizmos.color = Color.green;
        Gizmos.DrawLine(checkDistanceTransform.position, new Vector3(checkDistanceTransform.position.x + checkDistance * facingDirection, checkDistanceTransform.position.y));

        // Raycast do wykrywania przeciwników w zasiêgu ataku
        Gizmos.color = Color.red;
        Gizmos.DrawLine(attackRangeTransform.position, new Vector3(attackRangeTransform.position.x + attackRange * facingDirection, attackRangeTransform.position.y));
    }

    protected void CharacterDistanceCheck()
    {
        isCharacterDetected = Physics2D.Raycast(checkDistanceTransform.position, Vector2.right * facingDirection, checkDistance, whatIsCharacter);
        // Sprawdzaj wynik raycasta i podejmuj odpowiednie dzia³ania na postaci.
    }

    protected void enemyCheck()
    {
        isEnemyDetected = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemy);
        // Sprawdzaj wynik raycasta i podejmuj odpowiednie dzia³ania na postaci w zasiêgu ataku.
    }

    protected void baseCheck()
    {
        isEnemyBaseDetected = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemyBase);
    }

    protected void enemyUnitcheck()
    {
        isEnemyUnitDetected = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemyUnit);
    }

    protected void MoveCharacter()
    {
        if (!isDead)
        {
            isMoving = true;
            moveSpeed = 2;
            transform.position = transform.position + (Vector3.right * moveSpeed * facingDirection) * Time.deltaTime;
        }
    }

    protected void StopCharacter()
    {
        isMoving = false;
        moveSpeed = 0;
    }

    public float Damage
    {
        get { return damage; }
    }

    protected virtual void Attack()
    {
        // Wykonaj raycast, aby sprawdziæ obiekty w zasiêgu ataku
        RaycastHit2D hit = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemy);

        if (hit.collider != null)
        {
            // SprawdŸ, czy trafiony obiekt posiada komponent "Health"
            Health targetHealth = hit.collider.GetComponent<UnitHealth>();
            //Debug.Log(targetHealth.currentHealth);

            if (targetHealth != null)
            {
                // Zadaj obra¿enia trafionemu obiektowi
                targetHealth.TakeDamage(damage);
            }
        }
    }

    protected virtual void AttackBase()
    {
        // Wykonaj raycast, aby sprawdziæ obiekty w zasiêgu ataku
        RaycastHit2D hit = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemyBase);

        if (hit.collider != null)
        {
            // SprawdŸ, czy trafiony obiekt posiada komponent "Health"
            Health targetHealth = hit.collider.GetComponent<BaseHealth>();
            //Debug.Log(targetHealth.currentHealth);

            if (targetHealth != null)
            {
                // Zadaj obra¿enia trafionemu obiektowi
                targetHealth.TakeDamage(damage);
            }
        }
    }

    public virtual int GetPrice()
    {
        // Tutaj mo¿esz zaimplementowaæ logikê zwracaj¹c¹ cenê jednostki
        // Przyk³adowo, zak³adaj¹c, ¿e cena jest przechowywana jako pole klasy:
        return price;
    }

    public virtual int GetExpNeeded()
    {
        return expThreshold;
    }

    protected virtual void HandleDeath()
    {
        // Zmiana wartoœci isDead
        isDead = true;
        audioMenager.PlaySFX(audioMenager.death);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Dodaj tutaj kod do znikania hitboxów, animacji œmierci itp.
    }

    public void SetWinnerFlag()
    {
        isWinner = true;
    }

    protected virtual void HandleDeathAnimationEnd()
    {
        // Dodaj tutaj kod zwi¹zany z znikaniem hitboxów, itp.
        Destroy(gameObject);
    }
}