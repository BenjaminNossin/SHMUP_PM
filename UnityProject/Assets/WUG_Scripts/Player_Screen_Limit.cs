using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Screen_Limit : MonoBehaviour
{
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidht;
    private float objectHeight;
    [Range(-0.01f, -0.07f)] public float lowerbound = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidht = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidht, screenBounds.x - objectWidht);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * lowerbound + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
