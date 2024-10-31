using UnityEngine;

public class Laser : MonoBehaviour
{
    public BossAnimationController bossAnimationController;
    public int damage = 5; // Giá trị sát thương của Laser
    public Transform pivotPoint; // Điểm xoay
    public float rotationSpeed = 360f; // Tốc độ quay của laser
    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        // Lấy tham chiếu đến script của boss
        bossAnimationController = GetComponent<BossAnimationController>();
        bossAnimationController.PlayBossAnimation();
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Lấy script Main_Controller từ đối tượng nhân vật
            Main_Controller playerController = other.GetComponent<Main_Controller>();

            // Kiểm tra xem đối tượng nhân vật có Main_Controller không
            if (playerController != null)
            {
                // Gọi hàm giảm máu của nhân vật với giá trị sát thương từ Laser
                playerController.changeHealth(-damage);
            }
        }
    }
    void Update()
    {
        // Kiểm tra xem pivotPoint có tồn tại không
        if (pivotPoint != null)
        {
            // Xoay laser xung quanh pivotPoint theo trục Z
            transform.RotateAround(pivotPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            audioManager.PlaySFX(audioManager.bossLaser);
        }
        else
        {
            Debug.LogWarning("Please assign a pivot point for rotation.");
        }
     
    }
}




