using System.Collections;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject mainPanel;
    private bool paused = false;

    private void Awake()
    {
        pausePanel.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !paused)
        {
            paused = !paused;
            pausePanel.SetActive(paused);
            mainPanel.SetActive(!paused); 

            StartCoroutine(ListenInput());
            Time.timeScale = 0f;
        }
    }

    private IEnumerator ListenInput()
    {
        yield return new WaitForSecondsRealtime(0.01f);

        for (int i = 0; i < 100000; i++)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            if (Input.GetKeyDown(KeyCode.A) && paused)
            {
                Time.timeScale = 1f; 
                paused = !paused;
                pausePanel.SetActive(paused);
                mainPanel.SetActive(!paused);
                i = 100000; 
            }
        }
    }
}
