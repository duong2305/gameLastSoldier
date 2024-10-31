using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float totalTime = 60; // Đặt thời gian giới hạn ở đây (đơn vị: giây)
    private float currentTime = 0;
    public TMP_Text timeText;
    public int scene;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            DisplayTime(currentTime);
        }
        else
        {
            // Thời gian đã hết, thực hiện các hành động bạn muốn ở đây
            timeText.color = Color.red;
            HandleTimeUp();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    void HandleTimeUp()
    {
        SceneManager.LoadScene(scene);
    }
}
