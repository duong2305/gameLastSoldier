using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowWorm : MonoBehaviour
{
    public float slowDuration = 0.5f; // Thời gian làm chậm (giây)
    public float slowFactor = 2f; // Hệ số làm chậm (0.5 là mức độ làm chậm 50%)

    private void OnTriggerEnter2D(Collider2D other)
    {

        Main_Controller player = other.gameObject.GetComponent<Main_Controller>();
        if (player != null)
        {
            // Áp dụng hiệu ứng làm chậm cho nhân vật
            StartCoroutine(SlowPlayer(player));

            // Hủy đối tượng viên đạn băng
            Destroy(gameObject);
        }
    }

    IEnumerator SlowPlayer(Main_Controller player)
    {
        // Lưu lại tốc độ ban đầu của nhân vật
        float originalSpeed = player.speed;

        // Áp dụng làm chậm
        player.speed /= slowFactor;

        // Đợi cho đến khi hiệu ứng làm chậm kết thúc
        yield return new WaitForSeconds(slowDuration);
        Destroy(gameObject);
        // Khôi phục lại tốc độ ban đầu của nhân vật
        
    }
}
