using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(0.5f, 3f)] private float delayBeforeSelfDestroy = 2f; 
    [SerializeField] private string entityToDamage;
    [SerializeField, Range(1, 10)] private int damageAmount;

    public float speed = 20f;
    private Rigidbody2D rb;
    private void OnEnable()
    {
        Destroy(gameObject, delayBeforeSelfDestroy);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(entityToDamage))
        {
            Health temp = collision.GetComponent<Health>();
            temp.LoseHP(damageAmount);
        }
    }
}
