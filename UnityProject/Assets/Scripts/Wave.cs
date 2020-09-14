using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private AnimationCurve curveToFollow;
    private float evaluator;
    [Range(0f, 10f)] public int duration;
    [Range(0f, 10f)] public float amplitude;
    [Range(0f, 1f)] public float speedMultiplier;
 
    private float x, y;
    private Vector3 newPos; 

    private void Start()
    {
        evaluator = Mathf.Clamp(evaluator, 0f, 1f);
    }

    void FixedUpdate()
    {
        evaluator += Time.fixedDeltaTime; 
        newPos.x = curveToFollow.Evaluate(evaluator) * amplitude;
        newPos.y += Time.fixedDeltaTime * speedMultiplier;
        transform.position -= newPos;

        Destroy(gameObject, duration); 
    }
}
