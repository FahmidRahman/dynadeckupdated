using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                NPCPatroller npc = hit.collider.GetComponent<NPCPatroller>();
                if (npc != null)
                {
                    npc.StartConversation(transform); // Pass the player's transform to NPC
                }
            }
        }
    }
}
