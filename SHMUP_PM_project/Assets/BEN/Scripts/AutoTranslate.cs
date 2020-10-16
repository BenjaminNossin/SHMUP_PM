using System.Collections.Generic;
using UnityEngine;

public class AutoTranslate : MonoBehaviour
{
    [Range(1, 4)] public float speedMultiplier = 1;
    public List<Transform> transfToTranslate = new List<Transform>();

    private void OnEnable()
    {
        transform.position = new Vector2(-3.18f, -25f); 
    }

    void FixedUpdate()
    {
        foreach (var item in transfToTranslate)
        {
            item.position += Vector3.up * Time.fixedDeltaTime * speedMultiplier;
        }     
    }
}
