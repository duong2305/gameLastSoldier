using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int maxHealth = 4000;
    public int health;
    public Healthbar healthBarBoss;
    public Animator bossAnimator;
    public int damageP;
    public bool died=false;
    
    
    void Start()
    {
        health = maxHealth;
        healthBarBoss.UpdateBar(health, maxHealth);
        bossAnimator = GetComponent<Animator>();
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Boss took " + damage + " damage. Current health: " + health);
            UpdateHealthBar();
        }
    }

    public void ChangeHealth(int amount)    {
        health = Mathf.Clamp(health + amount, -10, maxHealth);
        Debug.Log("Boss health changed. Current health: " + health);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // Update the health bar on the UI
        healthBarBoss.UpdateBar(health, maxHealth);
    }

    void Die()
    {
        Debug.Log("Boss has been defeated!");
        died = true;
        SceneManager.LoadScene(9);
        // Perform necessary actions when the Boss is defeated
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider's tag is PlayerAttack
        if (other.CompareTag("PlayerAttack"))
        {
            // Assume the player attack causes a fixed amount of damage (you can modify this)
            
            TakeDamage(damageP);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Main_Controller player = other.gameObject.GetComponent<Main_Controller>();
        if (player != null)
        {
            player.changeHealth(-1);
            Debug.Log("Player health reduced.");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Main_Controller player = other.gameObject.GetComponent<Main_Controller>();
        if (player != null)
        {
            player.changeHealth(-1);
            Debug.Log("Player health reduced.");
        }
    }
    private void Update()
    {
        bossAnimator.SetBool("Died", died);
        
    }

}



    
    

    
    
