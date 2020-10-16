using System;
using System.Collections;
using UnityEditor;
using UnityEngine;


// download the csv data here
public class BasicAIBrain : MonoBehaviour
{
    public bool CanShoot { get; set; }
    private Transform enemyTransform;
    [SerializeField, Range(1f, 23f)] private float attackTreshold = 10f;
    [SerializeField] private DetectionZone zone;
    [SerializeField, Range(0.05f, 0.5f)] private float delay = 0.2f;
    private BoxCollider2D selfCollider;
    public bool boss = false;
    public AutoTranslate playerTranslate;

    public static Action OnBossDeath; 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - attackTreshold)); 
    }

    private void OnEnable()
    {
        CanShoot = false;
        selfCollider = GetComponent<BoxCollider2D>();
        selfCollider.enabled = false;
    }

    private void Start()
    {
        if (zone)
            StartCoroutine(DelayStop());

        if (boss)
        {
            playerTranslate.enabled = false; 
        }
    }

    private void Update()
    {
        if (enemyTransform == null)
        {
            try
            {
                enemyTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch (Exception) { }
        }
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
            CanShoot = Vector3.Distance(enemyTransform.position, transform.position) <= attackTreshold;
        }
        catch (Exception) { }

        if (CanShoot)
            selfCollider.enabled = true; 
    }

    private void OnDestroy()
    {
        if (boss)
        {
            OnBossDeath(); 
        }
    }
}
