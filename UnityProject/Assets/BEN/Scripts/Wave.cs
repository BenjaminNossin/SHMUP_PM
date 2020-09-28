using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private AnimationCurve curveToFollow;
    private float evaluator;
    [Range(0f, 10f)] public int duration;
    [Range(0f, 10f)] public float amplitude;
    [Range(0f, 1f)] public float speedMultiplier;
 
    private Vector3 newPos;

    private void OnEnable()
    {
        Destroy(gameObject, duration);
    }

    private void Start()
    {
        evaluator = Mathf.Clamp(evaluator, 0f, 1f);
    }

    void FixedUpdate()
    {
        evaluator += Time.fixedDeltaTime; 
        newPos.x = curveToFollow.Evaluate(evaluator) * amplitude * speedMultiplier;
        newPos.y += Time.fixedDeltaTime * speedMultiplier;
        transform.position -= newPos;
    }
}
