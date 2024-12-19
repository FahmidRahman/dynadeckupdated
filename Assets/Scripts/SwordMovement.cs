using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public float swingSpeed = 5f;
    public float swingRange = 30f;
    public float damage = 10f;
    public float damageRange = 1f;

    private Quaternion initialRotation;
    private bool isSwinging = false;
    private float swingProgress = 0f;

    private void Start()
    {
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSwinging = true;
            swingProgress = 0f;
        }

        if (isSwinging)
        {
            SwingSword();
        }
    }

    private void SwingSword()
    {
        swingProgress += Time.deltaTime * swingSpeed;
        float swingOffset = Mathf.Sin(swingProgress * Mathf.PI) * swingRange;
        transform.localRotation = initialRotation * Quaternion.Euler(swingOffset, 0f, 0f);

        if (swingProgress >= 1f)
        {
            isSwinging = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            float distanceToEnemy = Vector3.Distance(transform.position, other.transform.position);

            if (distanceToEnemy <= damageRange)
            {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();  // Ensure EnemyHealth script is attached to enemies
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }
}
