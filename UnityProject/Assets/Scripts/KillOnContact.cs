using System.Collections.Generic;
using UnityEngine;

// check the tag of the triggered element and kills it if allowed to
// only cares about the other entities
[RequireComponent(typeof(Collider2D))]
public class KillOnContact : MonoBehaviour
{
    public List<string> killableEntitiesTag = new List<string>();
    Object otherEntity;
    Health otherEntityHP;
    [SerializeField, Range(0f, 2f)] private float destructionDelay = 0f;

    private void OnTriggerEnter2D(Collider2D otherCollider2D)
    {
        otherEntity = otherCollider2D.gameObject;

        foreach (string item in killableEntitiesTag)
        {
            if (otherCollider2D.CompareTag(item))
            {
                otherEntityHP = otherCollider2D.GetComponent<Health>();
                if (otherEntityHP)
                    otherEntityHP.LoseHP(100);
                else
                {
                    Destroy(otherEntity, destructionDelay);
                }
            }
        }

        otherEntity = null;
    }
}
