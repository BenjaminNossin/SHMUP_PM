using System;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public TMP_Text TMPSCore;
    private int currentScore;
    [Range(2, 100)] public int scoreCeil = 50;
    [Range(1, 3)] public int HPGainMultiplier = 2;
    private int scoreCeilTracker;
    private int currentCeilTracker; 

    public static Action<int> OnScoreCeilReach;
    public static bool trackDone = false; 

    public void OnEnable()
    {
        Health.OnAIDeath += UpdateScore;
    }

    private void Start()
    {
        currentScore = 0;
        currentCeilTracker = 0;
        TMPSCore.text = $"Score : {currentScore}";
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

    void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd; 
        TMPSCore.text = $"Score : {currentScore}";      
    }

    public void OnDisable()
    {
        Health.OnAIDeath -= UpdateScore;
    }
}
