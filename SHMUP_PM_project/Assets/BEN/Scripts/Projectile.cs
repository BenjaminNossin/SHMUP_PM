using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// start moving on enable, then self destroy after a certain delay or if player is hit
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(0.5f, 3f)] private float delayBeforeSelfDestroy;
    [SerializeField] private string entityToDamage;
    [SerializeField, Range(1, 10)] private int damageAmount;

    private void OnEnable()
    {
        Destroy(gameObject, delayBeforeSelfDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(entityToDamage))
        {
            Health temp = collision.GetComponent<Health>();
            temp.LoseHP(damageAmount);
        }
    }
}
