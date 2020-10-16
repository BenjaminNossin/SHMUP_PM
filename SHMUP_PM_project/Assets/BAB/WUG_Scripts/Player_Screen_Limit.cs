using UnityEngine;

public class Player_Screen_Limit : MonoBehaviour
{
    public BoxCollider2D screenLimit;
    private Vector2 screenBounds;

    private float objectWidht; 
    private float objectHeight; 

    private void Start()
    {
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidht = transform.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f; 
    }

    private void LateUpdate() // otherwise it will glitch the sprite because clashing (Fixed)Update call
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenLimit.bounds.min.x + objectWidht, screenLimit.bounds.max.x - objectWidht);
        viewPos.y = Mathf.Clamp(viewPos.y, screenLimit.bounds.min.y + objectHeight, screenLimit.bounds.max.y - objectHeight);
        transform.position = viewPos; 
    }
}
