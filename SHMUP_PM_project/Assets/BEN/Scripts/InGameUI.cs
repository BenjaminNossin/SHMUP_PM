using System;
using System.Collections;
using TMPro;
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
    }
}
