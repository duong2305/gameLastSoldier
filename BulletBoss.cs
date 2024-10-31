using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;
    public Sprite bulletSprite; // Biến hình ảnh đạn

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Main_Controller player = other.GetComponent<Main_Controller>();
            if (player != null)
            {
                player.changeHealth(-damage);
            }

            Destroy(gameObject);
        }
    }
}
