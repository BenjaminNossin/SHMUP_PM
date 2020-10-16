using UnityEngine;

public class Player_Screen_Limit : MonoBehaviour
{
    public BoxCollider2D screenLimit; 

    void FixedUpdate()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, screenLimit.bounds.min.x, screenLimit.bounds.max.x),
                                         Mathf.Clamp(transform.position.y, screenLimit.bounds.min.y, screenLimit.bounds.max.y)); 
    }
}
