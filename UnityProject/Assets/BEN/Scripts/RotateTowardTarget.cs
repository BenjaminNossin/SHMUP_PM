using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateTowardTarget : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bodyToRotate;
    [SerializeField] private Aim targetToRotateToward; // add this via data stored in file 

    [SerializeField] private bool relativeRotation = false;

    void FixedUpdate()
    {
        Vector2 lookDir = targetToRotateToward.Position - bodyToRotate.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        if (relativeRotation)
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        else bodyToRotate.rotation = angle;
    }
}
