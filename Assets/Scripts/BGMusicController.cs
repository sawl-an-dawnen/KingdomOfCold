using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(AudioSource))]
public class BGMusicController : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioMixerGroup audioMixerGroup;
    private GameManager gameManager;
    public float songSpeed;
    public bool isCrunchTime = false;
    public const float WAIT_LIMIT = 1.0f;
    public float SLOW_SONG_SPEED_CAP = .66f;

    public float FAST_SONG_SPEED_CAP = 1.0f;
    public float FAST_SPEED_BUFFER = 1.5f;
    public float waitTime = WAIT_LIMIT;
    public float TIME_DURATION = 300f;
    private float crunchTime = 10.0f;
    private float crunchPercent = 0.2f;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        audioMixerGroup = audioSource.outputAudioMixerGroup;
        
        TIME_DURATION = gameManager.timeDuration * 1.2f;
        crunchTime = gameManager.crunchTime;
        crunchPercent = gameManager.crunchPercent;
        crunchTime = crunchPercent * gameManager.timeDuration;
        songSpeed = SLOW_SONG_SPEED_CAP;
        audioSource.pitch = songSpeed;
        audioMixerGroup.audioMixer.SetFloat("pitchBend", 1.0f / songSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrunchTime)
        {
            return;
        }
        if ((waitTime -= Time.deltaTime) <= 0.0f)
        {
            waitTime = WAIT_LIMIT;
            float timeRemaining = gameManager.getTime();
            if (timeRemaining <= crunchTime)
            {
                audioSource.pitch = 1.0f;
                audioMixerGroup.audioMixer.SetFloat("pitchBend", 1.5f);
                isCrunchTime = true;
            }
            else
            {
                songSpeed = GetSongSpeed(timeRemaining);
                audioSource.pitch = songSpeed;
                audioMixerGroup.audioMixer.SetFloat("pitchBend", 1.0f / songSpeed);
            }
        }
    }
    private float GetSongSpeed(float timeRemaining)
    {
        // crunch time * some constant so max speed isn't hit right when crunch time begins
        float slope = FindSlope(TIME_DURATION, crunchTime * FAST_SPEED_BUFFER, SLOW_SONG_SPEED_CAP, FAST_SONG_SPEED_CAP);
        float intercept = FindIntercept(slope, TIME_DURATION, SLOW_SONG_SPEED_CAP);
        float newSongSpeed = FindY(timeRemaining, slope, intercept);
        return Math.Max(SLOW_SONG_SPEED_CAP, Math.Min(FAST_SONG_SPEED_CAP, newSongSpeed));
    }
    private float FindSlope(float x1, float x2, float y1, float y2)
    {
        return (y2-y1)/(x2-x1);
    }
    private float FindIntercept(float m, float x, float y)
    {
        return y - (x * m);
    }
    private float FindY(float x, float m, float b)
    {
        return (x * m) + b;
    }
}
