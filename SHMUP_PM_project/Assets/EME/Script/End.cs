using UnityEngine;
using UnityEngine.SceneManagement;
public class End : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("LevelOneBEN");
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}

