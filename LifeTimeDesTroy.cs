using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeTimeDesTroy : MonoBehaviour
{
    AudioManager audioManager;
    public float time=2f;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Destroy(this.gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyAI player = other.gameObject.GetComponent<EnemyAI>();
        if (player != null)
        {
            player.changeHealth(-1);
            audioManager.PlaySFX(audioManager.bulletHit);
            Destroy(gameObject);
        }
    }
}
