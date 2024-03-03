using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool keepTime = true;
    public float winTimer = 3f;
    public GameObject[] toDestroy;
    // Start is called before the first frame update
    public float timeDuration = 1.3f * 60f;
    private float timer;
    private float flashTimer;
    private float flashDuration = 1f;
    private SceneLoader loader;

   
    [SerializeField] TextMeshProUGUI firstMinute;
    [SerializeField] TextMeshProUGUI secondMinute;
    [SerializeField] TextMeshProUGUI seperator;
    [SerializeField] TextMeshProUGUI firstSecond;
    [SerializeField] TextMeshProUGUI secondSecond;
    [SerializeField] TextMeshProUGUI milliSeperator;
    [SerializeField] TextMeshProUGUI firstMilli;
    [SerializeField] TextMeshProUGUI secondMilli;

    void Start()
    {
        ResetTimer();
        loader = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keepTime)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerDisplay(timer);
            }
            else
                Flash();
        }
        else
        {
            firstMinute.text = "";
            secondMinute.text = "";
            firstSecond.text = "";
            secondSecond.text = "";
            firstMilli.text = "";
            secondMilli.text = "";
        }
    }

    private void ResetTimer()
    {
        timer = timeDuration;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt( time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        string currentTime = string.Format("{00:00}{1:00}{2:000}", minutes, seconds, milliseconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
        firstMilli.text = currentTime[4].ToString();
        secondMilli.text = currentTime[5].ToString();
    }

    public void AddTime(float time)
    {
        timer += time;
    }
    private void Flash()
    {
        StartCoroutine(LoadNext());
        if(timer != 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        if(flashTimer <= 0)
        {
            flashTimer = flashDuration;
        } 
        else if (flashTimer >= flashDuration / 2) 
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }
        else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }
    }

    private void  SetTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;
        firstMilli.enabled = enabled;
        secondMilli.enabled = enabled;
    }

    private IEnumerator LoadNext()
    {
        foreach (GameObject obj in toDestroy) 
        {
            Destroy(obj);
        }
        // Find all objects tagged as "bullet"
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        // Destroy all bullets
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        yield return new WaitForSeconds(winTimer);
        loader.LoadScene();
    }
}
