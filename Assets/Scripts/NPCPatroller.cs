using UnityEngine;

public class NPCPatroller : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float waitTime = 1f;
    public Animator animator;
    public float rotationSpeed = 5f;
    public float stoppingDistance = 0.1f;
    public float conversationDuration = 3.0f;

    private int currentWaypointIndex = 0;
    private bool isWaiting = false;
    private bool movingForward = true;
    private bool isInConversation = false;
    private Transform playerTransform;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isInConversation)
        {
            FacePlayer();
            return; // Stop patrolling during conversation
        }

        // Patrolling behavior
        if (waypoints.Length == 0 || isWaiting) return;

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        float distanceToWaypoint = Vector3.Distance(transform.position, target.position);

        if (distanceToWaypoint > stoppingDistance)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            RotateTowards(target.position);
        }
        else
        {
            animator.SetBool("isWalking", false);
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private System.Collections.IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // Switch waypoints
        currentWaypointIndex = movingForward ? currentWaypointIndex + 1 : currentWaypointIndex - 1;

        if (currentWaypointIndex >= waypoints.Length)
        {
            movingForward = false;
            currentWaypointIndex = waypoints.Length - 1;
        }
        else if (currentWaypointIndex < 0)
        {
            movingForward = true;
            currentWaypointIndex = 0;
        }

        isWaiting = false;
    }

    public void StartConversation(Transform player)
    {
        isInConversation = true;
        playerTransform = player;
        StopAllCoroutines(); // Stop patrolling and waiting coroutines

        // Disable physics to prevent pushing by the player
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        animator.SetBool("isWalking", false); // Set idle animation
        StartCoroutine(EndConversationAfterDuration());
    }

    private System.Collections.IEnumerator EndConversationAfterDuration()
    {
        yield return new WaitForSeconds(conversationDuration);
        EndConversation();
    }

    public void EndConversation()
    {
        isInConversation = false;
        playerTransform = null;

        // Re-enable physics for NPC
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }

        // Resume patrol
        StartCoroutine(WaitAtWaypoint());
    }

    private void FacePlayer()
    {
        if (playerTransform == null) return;

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
