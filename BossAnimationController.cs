using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    AudioManager audioManager;
    void Start()
    {
        if (animator == null)
        {
            // Nếu animator không được gán từ trình chỉnh sửa Unity, thử tìm animator trong đối tượng hiện tại
            animator = GetComponent<Animator>();
        }
    }

    public void PlayBossAnimation()
    {
        // Kích hoạt animation khi laser xuất hiện
        if (animator != null)
        {
            animator.SetTrigger("BossAnimationTrigger");
           
        }
        else
        {
            Debug.LogError("Animator is not assigned to BossAnimationController.");
        }
    }
}
