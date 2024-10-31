
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio clip")]
    public AudioClip background;
    public AudioClip shoot;
    public AudioClip bossLaser;
    public AudioClip bulletHit;

    private void Start()
    {
        
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    
}
