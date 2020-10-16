using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public TMP_Text TMPScore;
    public TMP_Text TMPLevel;
    private int currentScore;
    [Range(2, 100)] public int scoreCeil = 50;
    [Range(1, 3)] public int HPGainMultiplier = 2;
    private int scoreCeilTracker;
    private int currentCeilTracker; 

    public static Action<int> OnScoreCeilReach;
    public static bool trackDone = false;
    public bool canFade = false;

    public PlayerScore playerData;
    public GameObject pausePanel;
    public GameObject mainPanel;

    public bool paused = false; 

    public void OnEnable()
    {
        Health.OnAIDeath += UpdateScore;
    }

    private void Start()
    {
        currentScore = 0;
        currentCeilTracker = 0;
        TMPScore.text = $"Score : {currentScore}";
        StartCoroutine(StartFade());
        pausePanel.SetActive(paused);
        mainPanel.SetActive(!paused); 
    }

    private void FixedUpdate()
    {
        if (canFade && TMPLevel.color.a > 0f)
            TMPLevel.color = new Vector4(1f, 1f, 1f, TMPLevel.color.a - Time.fixedDeltaTime * 1f);
    }

    private void Update()
    {
        scoreCeilTracker = Mathf.FloorToInt(currentScore / scoreCeil);
        trackDone = scoreCeilTracker == currentCeilTracker; 
        if (scoreCeilTracker > 0 && !trackDone)
        {
            trackDone = true; 
            OnScoreCeilReach(scoreCeilTracker * HPGainMultiplier); // each new ceil add a bigger amount of HP; 
            currentCeilTracker = scoreCeilTracker; 
        }

        if (Input.GetKeyDown(KeyCode.A) && !paused)
        {
            paused = !paused;
            pausePanel.SetActive(paused);
            mainPanel.SetActive(!paused);
            StartCoroutine(ResetBool());
            Time.timeScale = 0f; 

        }

    }

    IEnumerator ResetBool()
    {
        yield return new WaitForSecondsRealtime(0.01f);

        for (int i = 0; i < 10000; i++)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            if (Input.GetKeyDown(KeyCode.A) && paused)
            {
                paused = !paused;
                pausePanel.SetActive(paused);
                mainPanel.SetActive(!paused);
                Time.timeScale = 1f;
                i = 10000; 
            }
        }
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(1f);
        canFade = true; 
    }

    void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd; 
        TMPScore.text = $"Score : {currentScore}";      
    }

    public void OnDisable()
    {
        Health.OnAIDeath -= UpdateScore;
        PlayerScore.playerScore += currentScore;
        EditorUtility.SetDirty(playerData); 
    }
}
