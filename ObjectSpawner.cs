using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] gameObjects;
    public float spawnDelay = 10f;

    void Start()
    {
        StartCoroutine(SpawnObjectsNgauNhien());
    }

    IEnumerator SpawnObjectsNgauNhien()
    {
        while (true)
        {
            // Xáo trộn mảng các đối tượng
            System.Array.Sort(gameObjects, (x, y) => Random.Range(-1, 2));

            // Xuất hiện các đối tượng theo thứ tự ngẫu nhiên
            foreach (GameObject obj in gameObjects)
            {
                obj.SetActive(true); // Kích hoạt GameObject
                yield return new WaitForSeconds(spawnDelay); // Đợi trong khoảng thời gian spawnDelay

                obj.SetActive(false); // Vô hiệu hóa GameObject
                Debug.Log("Ok");
            }
        }
    }
}
