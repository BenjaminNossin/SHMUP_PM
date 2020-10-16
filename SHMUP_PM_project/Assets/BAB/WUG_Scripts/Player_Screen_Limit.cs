using UnityEngine;

public class Player_Screen_Limit : MonoBehaviour
{
    public BoxCollider2D screenLimit;
    private Vector2 objectSize; 

    private void Start()
    {
        objectSize = new Vector2(transform.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f,
                                transform.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f); 
    }

    private void LateUpdate() // otherwise it will glitch the sprite because clashing (Fixed)Update call
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenLimit.bounds.min.x + objectSize.x, screenLimit.bounds.max.x - objectSize.x);
        viewPos.y = Mathf.Clamp(viewPos.y, screenLimit.bounds.min.y + objectSize.y, screenLimit.bounds.max.y - objectSize.y);
        transform.position = viewPos; 
    }
}
