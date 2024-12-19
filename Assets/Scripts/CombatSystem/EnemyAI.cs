using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;  // How fast the enemy moves
    public float attackRange = 2f;  // Distance at which the enemy will attack the player
    public float attackCooldown = 1f;  // Time between enemy attacks
    public int attackDamage = 10;  // Damage dealt by the enemy

    private Transform player;  // Reference to the player's position
    private float nextAttackTime = 0f;  // Time at which the enemy can attack again

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;  // Assuming the player has the "Player" tag
    }

    private void Update()
    {
        MoveTowardsPlayer();

        // Attack player if in range and cooldown is over
        if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Move the enemy towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        // Apply damage to the player (assuming the player has a TakeDamage method)
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        
        // Set the next attack time
        nextAttackTime = Time.time + attackCooldown;
    }
}
