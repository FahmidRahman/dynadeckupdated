using UnityEngine;

public class RotatingRoomButton : MonoBehaviour
{
    public RoomRotator roomRotator; 
    private bool playerNearby = false;

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E)) 
        {
            roomRotator.RotateRoom();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerNearby = false;
        }
    }
}
