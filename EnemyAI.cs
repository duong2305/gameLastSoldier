using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO.Compression;
using UnityEngine.TextCore.Text;
using System.Threading;

public class EnemyAI : MonoBehaviour
{
    public bool roaming = true;
    public Seeker seeker;
    public bool updateContinuesPath;
    bool reachDestination = false;
    public float moveSpeed;
    public float nextWPDistance;
    public SpriteRenderer characterSR;
    Path path;
    Coroutine moveCoroutine;

    //Shoot
    public bool isShootable = false;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    private float fireCoolDown;

    public int health = 3;

    private float existTimer = 40.0f;

    public GameObject Explosion;

    public GameObject itemPrefab;
    public float dropProbability = 0.5f;

    private void Start()
    {
        Invoke("Disappear", existTimer);
        InvokeRepeating("CalculatePath", 0f, 0.1f);
        reachDestination = true;
    }

    private void Update()
    {
        
        fireCoolDown -= Time.deltaTime;
        if (fireCoolDown < 0)
        {
            fireCoolDown = timeBtwFire;
            //Shoot
            EnemyFireBullet();
        }
        
    }

    void EnemyFireBullet()
    {
        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindObjectOfType<Main_Controller>().transform.position;
        Vector3 direction = playerPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the bullet
        bulletTmp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

        // Set the scale of the bullet based on the direction
        Vector3 scale = bulletTmp.transform.localScale;
        scale.x *= Mathf.Sign(direction.x); // Flip the bullet if the player is on the left
        bulletTmp.transform.localScale = scale;
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();

        if(seeker.IsDone() && (reachDestination||updateContinuesPath))
            seeker.StartPath(transform.position, target, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        if(moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;
        while(currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if(distance < nextWPDistance)
            {
                currentWP++;
            }
            if (force.x != 0)
                if (force.x < 0)
                    characterSR.transform.localScale = new Vector3(-1, 1, 0);
                else
                    characterSR.transform.localScale = new Vector3(1, 1, 0);
            yield return null;
        }
        reachDestination = true;
    }

    Vector2 FindTarget()
    {
        Vector3 playerPos = FindObjectOfType<Main_Controller>().transform.position;
        if(roaming == true)
        {
            return (Vector2)playerPos + (Random.Range(0f, 3f) * new Vector2(Random.Range(-1,1), Random.Range(-1,1)).normalized);
        }
        else
        {
            return playerPos;          
        }
    }

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        Main_Controller player = other.gameObject.GetComponent<Main_Controller>();
        if (player != null)
        {
            player.changeHealth(-1);
            Debug.Log("fff");
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        Main_Controller player = other.gameObject.GetComponent<Main_Controller>();
        if (player != null)
        {
            player.changeHealth(-1);
            Debug.Log("fff");
        }
    }

    public void changeHealth(int amount)
    {       
        health = Mathf.Clamp(health + amount, 0, 3);
        if(health == 0)
        {
            
            Destroy(gameObject);
            DropItem();
            Instantiate(Explosion, transform.position, Quaternion.identity);
        } 
    }
    void DropItem()
    {
        if (itemPrefab != null && Random.value < dropProbability)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }

    void Disappear()
    {
        // Ẩn đối tượng
        Destroy(gameObject);
        // Hoặc có thể sử dụng Destroy(gameObject) để hủy đối tượng nếu bạn muốn nó biến mất hoàn toàn
    }
}
