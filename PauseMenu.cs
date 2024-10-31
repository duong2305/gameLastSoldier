using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
