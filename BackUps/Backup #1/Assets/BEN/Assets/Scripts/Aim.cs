using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Transform targetToAim;
    public Vector2 Position { get; set; }

    private void Update()
    {
        if (targetToAim != null)
            Position = targetToAim.position;
    }
}
