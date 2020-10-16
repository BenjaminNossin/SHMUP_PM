using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

