using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    AsyncOperation asyncOp;
    private bool playerDetected = false;
    public AutoTranslate autoTranslate;
    public GameObject nextLevel;

    public bool bossLevel = false; 

    private void OnEnable()
    {
        if (bossLevel)
            BasicAIBrain.OnBossDeath += LoadWinScreen; 

        if(SceneManager.GetActiveScene().buildIndex > 1 && SceneManager.GetActiveScene().buildIndex < 5)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1); 
        }
    }

    private void Start()
    {
        if (!bossLevel)
            nextLevel.SetActive(false); 
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
            autoTranslate.enabled = false;

            if (!bossLevel)
                nextLevel.SetActive(true); 

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                asyncOp = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
                asyncOp.allowSceneActivation = false;
                playerDetected = true; 
            }
        }
    }

    void ManageNextLevel()
    {
        if (asyncOp.progress >= 0.9f)
        {
            asyncOp.allowSceneActivation = true;
        }
    }

    void LoadWinScreen()
    {
        asyncOp = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        asyncOp.allowSceneActivation = false;
        playerDetected = true;

        if (asyncOp.progress >= 0.9f)
        {
            asyncOp.allowSceneActivation = true;
            StartCoroutine(UnloadBossScene()); 
        }
    }

    IEnumerator UnloadBossScene()
    {
        yield return new WaitForFixedUpdate(); 
        SceneManager.UnloadSceneAsync("BossSceneBEN", UnloadSceneOptions.None); 
    }

    private void OnDisable()
    {
        if (bossLevel)
            BasicAIBrain.OnBossDeath -= LoadWinScreen;
    }
}
