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
    [SerializeField] protected float damage; // Ilo�� obra�e� zadawanych przez jednostk�
    [SerializeField] protected float attackCooldown; // Czas oczekiwania mi�dzy atakami
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
        // Zasubskrybuj metod� HandleDeath() do zdarzenia �mierci
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
                // Je�li to jest pierwszy atak, ustaw flag� hasStartedAttacking na true i zainicjuj startTime
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
                isAttacking = false; // Po wykonaniu ataku resetuj flag�
            }
        }
        else
        {
            hasStartedAttacking = false; // Zresetuj flag�, gdy nie ma wrog�w w zasi�gu
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
        // Raycast do wykrywania sojusznik�w i zatrzymania postaci
        Gizmos.color = Color.green;
        Gizmos.DrawLine(checkDistanceTransform.position, new Vector3(checkDistanceTransform.position.x + checkDistance * facingDirection, checkDistanceTransform.position.y));

        // Raycast do wykrywania przeciwnik�w w zasi�gu ataku
        Gizmos.color = Color.red;
        Gizmos.DrawLine(attackRangeTransform.position, new Vector3(attackRangeTransform.position.x + attackRange * facingDirection, attackRangeTransform.position.y));
    }

    protected void CharacterDistanceCheck()
    {
        isCharacterDetected = Physics2D.Raycast(checkDistanceTransform.position, Vector2.right * facingDirection, checkDistance, whatIsCharacter);
        // Sprawdzaj wynik raycasta i podejmuj odpowiednie dzia�ania na postaci.
    }

    protected void enemyCheck()
    {
        isEnemyDetected = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemy);
        // Sprawdzaj wynik raycasta i podejmuj odpowiednie dzia�ania na postaci w zasi�gu ataku.
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
        // Wykonaj raycast, aby sprawdzi� obiekty w zasi�gu ataku
        RaycastHit2D hit = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemy);

        if (hit.collider != null)
        {
            // Sprawd�, czy trafiony obiekt posiada komponent "Health"
            Health targetHealth = hit.collider.GetComponent<UnitHealth>();
            //Debug.Log(targetHealth.currentHealth);

            if (targetHealth != null)
            {
                // Zadaj obra�enia trafionemu obiektowi
                targetHealth.TakeDamage(damage);
            }
        }
    }

    protected virtual void AttackBase()
    {
        // Wykonaj raycast, aby sprawdzi� obiekty w zasi�gu ataku
        RaycastHit2D hit = Physics2D.Raycast(attackRangeTransform.position, Vector2.right * facingDirection, attackRange, whatIsEnemyBase);

        if (hit.collider != null)
        {
            // Sprawd�, czy trafiony obiekt posiada komponent "Health"
            Health targetHealth = hit.collider.GetComponent<BaseHealth>();
            //Debug.Log(targetHealth.currentHealth);

            if (targetHealth != null)
            {
                // Zadaj obra�enia trafionemu obiektowi
                targetHealth.TakeDamage(damage);
            }
        }
    }

    public virtual int GetPrice()
    {
        // Tutaj mo�esz zaimplementowa� logik� zwracaj�c� cen� jednostki
        // Przyk�adowo, zak�adaj�c, �e cena jest przechowywana jako pole klasy:
        return price;
    }

    public virtual int GetExpNeeded()
    {
        return expThreshold;
    }

    protected virtual void HandleDeath()
    {
        // Zmiana warto�ci isDead
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

        // Dodaj tutaj kod do znikania hitbox�w, animacji �mierci itp.
    }

    public void SetWinnerFlag()
    {
        isWinner = true;
    }

    protected virtual void HandleDeathAnimationEnd()
    {
        // Dodaj tutaj kod zwi�zany z znikaniem hitbox�w, itp.
        Destroy(gameObject);
    }
}