using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("LevelOneBEN", LoadSceneMode.Single); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

