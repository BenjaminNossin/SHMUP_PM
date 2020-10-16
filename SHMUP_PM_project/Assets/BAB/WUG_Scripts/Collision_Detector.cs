using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Detector : MonoBehaviour
{
    public LayerMask playerMask;
    private Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Exit");
            position = collision.transform.position;
            if (collision.transform.position.x > transform.position.y)
            {
                collision.transform.position = new Vector2(collision.transform.position.x * -1, collision.transform.position.y);
            }
            else
            {
                collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y * -1);
            }
        }
    }
}
