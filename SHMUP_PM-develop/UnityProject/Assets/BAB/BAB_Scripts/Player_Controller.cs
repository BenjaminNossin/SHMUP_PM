using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 10f;

    public int maxHealth = 5;
    public int currentHealth;

    public Player_HealthBar healthBar;
    public Life_Counter lifeCounter;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        lifeCounter = GetComponent<Life_Counter>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Déplacement du vaisseau
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, verticalInput * moveSpeed * Time.deltaTime, 0);

        // Test Mort
        Died();

        // Test Damage
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Died()
    {
        if (currentHealth <= 0)
        {
            Life_Counter.lifeValue -= 1;
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }

        if (Life_Counter.lifeValue <= 0)
        {
            currentHealth = 0;
            healthBar.SetHealth(currentHealth);
            Life_Counter.lifeValue = 0;
            animator.SetTrigger("Died");
            moveSpeed = 0f;
            SceneManager.LoadScene("GameOver_Scene");
        }
    }
}