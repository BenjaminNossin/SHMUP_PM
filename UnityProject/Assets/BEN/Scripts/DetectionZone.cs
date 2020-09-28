using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Behaviour> behavioursToActivate;
    public string tagToCompare;
    public bool translate = false;
    public Transform entityToTranslate;
    public float speedMultiplier = 2f; 

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Behaviour item in behavioursToActivate)
            item.enabled = false; 
    }

    private void FixedUpdate()
    {
        if (translate)
        {
            Vector3 temp = Vector3.down * Time.fixedDeltaTime * speedMultiplier; 
            transform.Translate(temp, Space.World); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToCompare))
            try
            {
            foreach (Behaviour item in behavioursToActivate)
                item.enabled = true;
            }
            catch (NullReferenceException) { }
    }
}
