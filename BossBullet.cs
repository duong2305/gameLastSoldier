using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform firePos; // Vị trí xuất phát đạn
    public GameObject bulletPrefab; // Prefab của đạn
    public float bulletForce = 10f; // Lực của đạn

    public float fireRate = 1f; // Tốc độ bắn đạn (số lần bắn trong mỗi giây)
    private float nextFireTime = 0f; // Thời điểm bắn đạn tiếp theo

    
   

    void Update()
    {
        // Kiểm tra nếu đến lúc bắn đạn
        if (Time.time >= nextFireTime)
        {
            // Gọi hàm bắn đạn
            Shoot();

            // Cập nhật thời điểm bắn đạn tiếp theo
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // Kiểm tra xem có Prefab của đạn và vị trí xuất phát đạn không
        if (bulletPrefab != null && firePos != null)
        {
            // Tạo một góc ngẫu nhiên cho đạn
            float randomAngle = Random.Range(-45f, 45f);

            // Instantiate một đạn tại vị trí xuất phát và quay theo hướng của quái vật và góc ngẫu nhiên
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation * Quaternion.Euler(0f, 0f, randomAngle));
            

            // Lấy component Rigidbody2D của đạn và thêm lực cho nó
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                // Thêm lực cho đạn
                bulletRb.AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Main_Controller player = other.gameObject.GetComponent<Main_Controller>();
        if (player != null)
        {
            Debug.Log("ok");
            player.changeHealth(-1);
            Destroy(gameObject);
        }
    }
}
