using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("level1"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsScene");
    }
}
