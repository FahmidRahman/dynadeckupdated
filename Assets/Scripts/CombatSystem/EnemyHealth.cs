using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;  // Starting health of the enemy

    // Function to take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health left: {health}");

        // If health reaches 0, destroy the enemy
        if (health <= 0)
        {
            Die();
        }
    }

    // Function to handle the enemy's death
    void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);  // Destroy the enemy object
    }
}
