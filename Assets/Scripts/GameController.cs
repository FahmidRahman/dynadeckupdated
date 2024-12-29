using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Replace with your dungeon scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
