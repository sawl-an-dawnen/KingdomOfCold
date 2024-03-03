using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool keepTime = true;
    public float winTimer = 3f;
    public GameObject[] toDestroy;
    // Start is called before the first frame update
    public float timeDuration = 1.3f * 60f;
    public float crunchTime = 10.0f;
    public float crunchPercent = 0.2f;
    public bool isCrunchTime = false;
    private const float PROMPT_FLASH_RANGE = 0.5f;
    private float promptFlashTimer;
    private bool promptIsRed = false;
    public float promptOpacity;
    private float timer;
    private float flashTimer;
    private float flashDuration = 1f;
    private SceneLoader loader;

    private bool shieldActivated = false;
    private RawImage shieldBurst;
    public float fadeAway = 5f;
    private AudioSource gameWinSound;


    [SerializeField] TextMeshProUGUI firstMinute;
    [SerializeField] TextMeshProUGUI secondMinute;
    [SerializeField] TextMeshProUGUI seperator;
    [SerializeField] TextMeshProUGUI firstSecond;
    [SerializeField] TextMeshProUGUI secondSecond;
    [SerializeField] TextMeshProUGUI milliSeperator;
    [SerializeField] TextMeshProUGUI firstMilli;
    [SerializeField] TextMeshProUGUI secondMilli;
    [SerializeField] TextMeshProUGUI prompt;

    void Start()
    {
        ResetTimer();
        loader = GetComponent<SceneLoader>();
        shieldBurst = GameObject.FindGameObjectWithTag("OSB").GetComponent<RawImage>();
        gameWinSound = GetComponent<AudioSource>();
        promptFlashTimer = PROMPT_FLASH_RANGE;
        prompt = GameObject.FindGameObjectWithTag("Prompt").GetComponent<TextMeshProUGUI>();
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
                UpdatePrompt();
            }
            else
            {
                Flash();

                if (shieldActivated)
                {
                    shieldBurst.color = new Color(1f, 1f, 1f, (shieldBurst.color.a - (5f * Time.deltaTime)));
                }
                else 
                {
                    shieldActivated = true;
                    shieldBurst.color = new Color(1f, 1f, 1f, 1f);
                    gameWinSound.Play();
                }
            }
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
    
    public float getTime()
    {
        return timer;
    }
    
    public void UpdatePrompt()
    {
        if (timer <= crunchTime && !isCrunchTime)
        {
            isCrunchTime = true;
            return;
        }
        else if (isCrunchTime)
        {
            if ((promptFlashTimer -= Time.deltaTime) <= 0.0f)
            {
                promptFlashTimer = PROMPT_FLASH_RANGE;
                if (promptIsRed)
                {
                    prompt.color = Color.white;
                    promptIsRed = false;
                }
                else
                {
                    prompt.color = Color.red;
                    promptIsRed = true;
                }
            }
        }
        else
        {
            promptOpacity = Math.Max(0.0f, Math.Min(1.0f, (timeDuration - timer) / timeDuration));
            prompt.alpha = promptOpacity;
        }
    }
}
