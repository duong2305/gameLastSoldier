using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Main_Controller : MonoBehaviour
{
    public float speed = 1f;
    public float moveSpeed = 1.6f;
    public int maxHealth = 20;
    public int Health;
    public int playerDamage = 10;
    Rigidbody2D rb;
    Vector2 moveInput;
    float horizontal;
    float vertical;
    public Animator animator;
    public float timeInvincible = 1.0f;
    float invincibleTimer;
    bool isInvincible;
    public Boss boss; // Thêm một trường để kết nối với script của Boss
    public Healthbar healthBar;
    bool died = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = maxHealth;
        animator = GetComponent<Animator>();
        healthBar.UpdateBar(Health, maxHealth);
    }

     public void ApplySlow(float duration, float slowFactor)
    {
        // Áp dụng làm chậm
        moveSpeed *= slowFactor;
        StartCoroutine(RemoveSlowAfterDelay(duration));
    }

    private IEnumerator RemoveSlowAfterDelay(float delay)
    {
        // Đợi một khoảng thời gian sau đó loại bỏ làm chậm
        yield return new WaitForSeconds(delay);

        RemoveSlow();
    }

    public void RemoveSlow()
    {
        // Loại bỏ làm chậm, khôi phục tốc độ di chuyển gốc
        moveSpeed = speed;
    }

    public void OnDeathAnimationComplete()
    {
        // Biến mất nhân vật hoặc thực hiện bất kỳ hành động nào sau khi death animation kết thúc
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        float overallSpeed = Mathf.Sqrt(horizontal * horizontal + vertical * vertical);
        animator.SetFloat("Speed", overallSpeed);
        animator.SetBool("Died", died);
        if (Health == 0)
        {
            died = true;
        }
        if (horizontal != 0)
        {
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
        Isdied();
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontal, vertical).normalized * moveSpeed * Time.deltaTime;

        // Kiểm tra nếu có đầu vào từ người chơi trước khi cập nhật vị trí
        if (Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f)
        {
            rb.MovePosition(rb.position + movement);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            // Khi nhân vật chạm vào Boss, giảm máu của nhân vật
            changeHealth(-1);

            // Gọi hàm giảm máu của Boss từ script của Boss và truyền giá trị sát thương của nhân vật
            boss.ChangeHealth(-playerDamage);
        }
    }

    public void changeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        Health = Mathf.Clamp(Health + amount, 0, maxHealth);
        healthBar.UpdateBar(Health, maxHealth);
        Debug.Log("OK");
    }
    public void Isdied()
    {
        if (Health == 0)
        {
            SceneManager.LoadScene(8);
        }
    }
}
