using UnityEngine;

public class SpiderSwirl : MonoBehaviour
{
    public Transform swirlCenter;  // Central point for the swirl
    public float swirlSpeed = 5f; // Speed of the swirl
    public float radius = 2f;     // Distance from the center
    public float verticalOffset = 0.5f; // Vertical oscillation height
    public float verticalSpeed = 2f;    // Speed of vertical oscillation

    private float angle = 0f;  // Current angle of rotation

    void Update()
    {
        if (swirlCenter != null)
        {
            // Increment the angle based on time and swirl speed
            angle += swirlSpeed * Time.deltaTime;
            if (angle >= 360f) angle -= 360f;

            // Calculate the new position
            float x = swirlCenter.position.x + Mathf.Cos(angle) * radius;
            float z = swirlCenter.position.z + Mathf.Sin(angle) * radius;
            float y = swirlCenter.position.y + Mathf.Sin(Time.time * verticalSpeed) * verticalOffset;

            // Apply the calculated position to the spider
            transform.position = new Vector3(x, y, z);

            // Rotate the spider to face the center
            transform.LookAt(swirlCenter);
        }
    }
}
