using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeDuration = 1.3f * 60f;
    private float timer;

   
    [SerializeField] TextMeshProUGUI firstMinute;
    [SerializeField] TextMeshProUGUI secondMinute;
    [SerializeField] TextMeshProUGUI seperator;
    [SerializeField] TextMeshProUGUI firstSecond;
    [SerializeField] TextMeshProUGUI secondSecond;

    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
      if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
      else
        Flash();
    }

    private void ResetTimer()
    {
        timer = timeDuration;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt( time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    private  void Flash()
    {

    }
}
