using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 10f;

    public static int maxHealth = 10;
    private int currentHealth;

    public Player_HealthBar healthBar;
    public Life_Counter lifeCounter;
    public Animator animator;

    public bool invulnerable = false; 

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Déplacement du vaisseau
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, verticalInput * moveSpeed * Time.deltaTime, 0);

        if (!invulnerable)
            Died();
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            currentHealth -= damage;
            healthBar.UpdateHealth(currentHealth);
        }
    }

    void Died()
    {
        if (currentHealth <= 0)
        {
            Life_Counter.lifeValue -= 1;
            currentHealth = maxHealth;
            healthBar.UpdateHealth(currentHealth);
        }

        if (Life_Counter.lifeValue <= 0)
        {
            currentHealth = 0;
            healthBar.UpdateHealth(currentHealth);
            Life_Counter.lifeValue = 0;
            animator.SetTrigger("Died");
            moveSpeed = 0f;
            StartCoroutine(LoadGameOver()); 
        }
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("GameOver_Scene", LoadSceneMode.Single);
    }
}