using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    // Phương thức được gọi khi bắt đầu
    public void ChoiMoi()
    {
        SceneManager.LoadScene(1);
        
    }

    // Phương thức tạm dừng
    

    // Phương thức thoát game
    public void Thoat()
    {
        SceneManager.LoadScene(0);
    }

    // Phương thức quay lại màn chơi chính
    public void Quaman12() {
        SceneManager.LoadScene(3);
    }
    public void quaman23()
    {
        SceneManager.LoadScene(5);
    }
    public void quaman34()
    {
        SceneManager.LoadScene(7);
    }
}
