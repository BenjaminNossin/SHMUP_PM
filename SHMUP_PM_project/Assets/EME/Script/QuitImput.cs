using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitImput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
