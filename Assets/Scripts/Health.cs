using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Dodaj kod, który ma byæ wykonany po œmierci jednostki
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}