using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Range(1f, 60f)] public int MaxHP = 3;
    public int CurrentHP { get; set; }

    [Range(0.05f, 2f)] public float deathDelay = 0.1f;
    UnityEngine.Object self;
    public bool invulnerable = false;
    public bool dropOnDeath = false;

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
        if (dropOnDeath)
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

        }

        self = transform.gameObject;
        Destroy(gameObject, deathDelay);
        self = null;
    }

    public void LoseHP(int amount)
    {
        CurrentHP -= amount;
    }
}
