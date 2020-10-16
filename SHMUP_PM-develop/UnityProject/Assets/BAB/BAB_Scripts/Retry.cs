using UnityEngine;
using UnityEngine.SceneManagement;
public class Retry : MonoBehaviour
{
    public void RetryButton()
    {
        SceneManager.LoadScene("LevelOneBEN");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
