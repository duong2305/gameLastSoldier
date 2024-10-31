using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormBulletS : MonoBehaviour
{
    public bool applySlowEffect = true; // Set to true if you want to apply the slowing effect
    public float slowDuration = 2f;      // Duration of the slowing effect
    public float slowFactor = 0.5f;      // Slow factor applied to the player's movement speed

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Main_Controller player = other.gameObject.GetComponent<Main_Controller>();
        if (player != null)
        {
            player.changeHealth(-1);

            // Check if the applySlowEffect is true before applying the slowing effect
            if (applySlowEffect)
            {
                // Apply the slowing effect to the player
                StartCoroutine(SlowPlayer(player));
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }

    IEnumerator SlowPlayer(Main_Controller player)
    {
        // Apply the slow effect to the player
        player.ApplySlow(slowDuration, slowFactor);

        // Wait for the specified duration
        yield return new WaitForSeconds(slowDuration);

        // Remove the slow effect after the duration
        player.RemoveSlow();
    }
}
