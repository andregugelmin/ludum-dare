using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public int timerMin;
    public float timerSec;
    public TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        if(timerSec <= 0)
        {
            timerSec = 59;
            timerMin -= 1;
        }
        timerSec -= 1 * Time.deltaTime;

        if (Mathf.RoundToInt(timerSec) < 10)
        {
            timerText.text = (timerMin + ":0" + Mathf.RoundToInt(timerSec));
        }
        else
        {
            timerText.text = (timerMin + ":" + Mathf.RoundToInt(timerSec));
        }

        if (timerMin < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
