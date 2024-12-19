using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;  // Starting health of the player

    // Function to take damage from enemies
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage} damage. Health left: {health}");

        // If health reaches 0, handle player death (e.g., restart game or display game over)
        if (health <= 0)
        {
            Die();
        }
    }

    // Function to handle the player's death
    void Die()
    {
        Debug.Log("Player has died.");
        // Here you can trigger a game over screen, restart, etc.
    }
}
