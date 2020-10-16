using System;
using System.Collections;
using UnityEditor;
using UnityEngine;


// download the csv data here
public class BasicAIBrain : MonoBehaviour
{
    public bool CanShoot { get; set; }
    private GameObject enemy;
    [SerializeField, Range(1f, 23f)] private float attackTreshold = 10f;
    [SerializeField] private DetectionZone zone;
    [SerializeField, Range(0.05f, 0.5f)] private float delay = 0.2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - attackTreshold)); 
    }

    private void Awake()
    {
        CanShoot = false;
    }

    private void OnEnable()
    {
        if (zone)
            StartCoroutine(DelayStop()); 

        if (enemy == null)
            enemy = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/BEN/Prefabs/Ship.prefab", typeof(GameObject)); 
    }

    IEnumerator DelayStop()
    {
        yield return new WaitForSeconds(delay); 
        zone.translate = false;
    }

    private void FixedUpdate()
    {
        try
        {
            CanShoot = Vector3.Distance(enemy.transform.position, transform.position) <= attackTreshold;
        }
        catch (NullReferenceException) { }
    }
}
