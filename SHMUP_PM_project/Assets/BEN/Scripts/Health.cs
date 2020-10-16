using System;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Range(1f, 100f)] public int MaxHP = 3;
    public int CurrentHP { get; set; }

    [Range(0.05f, 2f)] public float deathDelay = 0.1f;
    UnityEngine.Object self;
    public bool invulnerable = false;
    public bool dropOnDeath = false;

    public static Action<int> OnAIDeath;
    private bool done = false;

    public bool playerHP = false;

    public void OnEnable()
    {
        if (playerHP)
            InGameUI.OnScoreCeilReach += AddHP; 
    }

    private void Awake()
    {
        CurrentHP = MaxHP;
    }

    public void Update()
    {
        if (CurrentHP <= 0 && !invulnerable)
            Death();
    }

    public void Death()
    {
        if (dropOnDeath && !done)
        {
            try
            {
                RandomDrop temp = GetComponent<RandomDrop>();

                if (!temp)
                {
                    RandomDrop temp2 = GetComponentInParent<RandomDrop>();

                    if (!temp2)
                    {
                        RandomDrop temp3 = GetComponentInChildren<RandomDrop>();
                    }
                }

                temp.Drop();
                Debug.Log("DEATH");
            }
            catch (Exception)
            {
                Debug.LogError("No randomDrop component found");
            }

            try
            {
                int score = GetComponent<Score>().scoreCount;
                OnAIDeath(score); 
            }
            catch (Exception) { }

        }

        self = transform.gameObject;
        Destroy(gameObject, deathDelay);
        self = null;
        done = true; 
    }

    public void LoseHP(int amount)
    {
        CurrentHP -= amount;
    }

    public void AddHP(int amount)
    {
        CurrentHP += amount;
        MaxHP = CurrentHP;
    }

    public void OnDisable()
    {
        if (playerHP)
        {
            InGameUI.OnScoreCeilReach -= AddHP;
        }
    }
}
