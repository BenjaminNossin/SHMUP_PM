using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    AsyncOperation asyncOp;
    private bool playerDetected = false;

    private void OnEnable()
    {
        BasicAIBrain.OnBossDeath += ManageNextLevel; 
    }

    public void FixedUpdate()
    {
        if (playerDetected)
            ManageNextLevel(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            asyncOp = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
            asyncOp.allowSceneActivation = false;
            playerDetected = true; 
        }
    }

    void ManageNextLevel()
    {
        if (asyncOp.progress >= 0.9f)
        {
            asyncOp.allowSceneActivation = true;
            StartCoroutine(UnloadScene());
        }
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForFixedUpdate();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene(), UnloadSceneOptions.None); 
    }

    private void OnDisable()
    {
        BasicAIBrain.OnBossDeath -= ManageNextLevel;
    }
}
