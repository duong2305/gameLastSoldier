using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}
