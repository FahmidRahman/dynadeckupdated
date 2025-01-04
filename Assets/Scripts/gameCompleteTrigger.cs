using UnityEngine;

public class gameCompleteTrigger : MonoBehaviour
{
    public GameObject gameCompleteMessage; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
           
            gameCompleteMessage.SetActive(true);
            Time.timeScale = 0f; 
        }
    }
}
