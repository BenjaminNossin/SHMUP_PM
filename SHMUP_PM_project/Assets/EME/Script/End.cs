using UnityEngine;
using UnityEngine.SceneManagement;
public class End : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

