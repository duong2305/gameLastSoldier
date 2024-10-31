using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPortion : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Main_Controller player = other.gameObject.GetComponent<Main_Controller>();
        if (player.Health<player.maxHealth)
        {
            if (player != null)
            {
                player.changeHealth(2);
                Destroy(gameObject);
            } 
        }
    }

}
