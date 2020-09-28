using UnityEngine;


// download the csv data here
public class BasicAIBrain : MonoBehaviour
{
    public bool CanShoot { get; set; }
    [SerializeField] private Transform enemy;
    [SerializeField, Range(5f, 20f)] private float attackTreshold = 8f;

    private void Awake()
    {
        CanShoot = false;
    }

    private void FixedUpdate()
    {
        CanShoot = Vector3.Distance(enemy.position, transform.position) <= attackTreshold;
    }
}
