using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attackDamage = 10;  // Damage dealt by the player
    public float attackRange = 2f;  // Distance within which the player can hit enemies
    public LayerMask enemyLayer;  // A layer mask to specify which objects are considered enemies
    public Transform attackPoint;  // The point from which the attack is launched (player's hand or weapon)

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Attack when Space key is pressed
        {
            Attack();
        }
    }

    // Function to handle the attack logic
    void Attack()
    {
        // Detect enemies within the attack range
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (var enemy in hitEnemies)
        {
            // Apply damage to each enemy that is hit
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }
}
