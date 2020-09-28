using UnityEngine;


// download the csv data here
public class BasicAIBrain : MonoBehaviour
{
    public bool CanShoot { get; set; }
    [SerializeField] private Transform enemy;
    [SerializeField, Range(1f, 23f)] private float attackTreshold = 10f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - attackTreshold)); 
    }

    private void Awake()
    {
        CanShoot = false;
    }

    private void FixedUpdate()
    {
        CanShoot = Vector3.Distance(enemy.position, transform.position) <= attackTreshold;
    }
}
