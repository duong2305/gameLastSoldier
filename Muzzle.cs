using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public SpriteRenderer muzzleSR;

    // Start is called before the first frame update
    void Start()
    {
        muzzleSR.enabled = false; // Initially hide the SpriteRenderer
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            // Show the SpriteRenderer
            muzzleSR.enabled = true;
        }
        else
        {
            // Hide the SpriteRenderer
            muzzleSR.enabled = false;
        }
    }
}
