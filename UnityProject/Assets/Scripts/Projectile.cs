using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// start moving on enable, then self destroy after a certain delay or if player is hit
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(0.5f, 3f)] private float delayBeforeSelfDestroy;
    [SerializeField] private string entityToDamage;
    [SerializeField, Range((int)1f, (int)3f)] private int damageAmount;

    private void OnEnable()
    {
        Destroy(gameObject, delayBeforeSelfDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(entityToDamage))
        {
            Health temp = GetComponent<Health>();
            temp.LoseHP(damageAmount);
        }
    }
}
