using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainSceneBEN");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

