using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public Image fillBar;
    public TextMeshProUGUI valueText;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    public void UpdateBar(int currentValue, int maxValue)
    {
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
        valueText.text = currentValue.ToString() + " / " + maxValue.ToString();
    }
}
