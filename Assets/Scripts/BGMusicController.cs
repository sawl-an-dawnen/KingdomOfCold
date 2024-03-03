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
    public float songSpeed;
    public bool isCrunchTime = false;
    public const float WAIT_LIMIT = 1.0f;
    [Tooltip("1.0f is the intended speed")]
    public float SLOW_SONG_SPEED_CAP = .66f;

    [Tooltip("2.0 = twice as fast")]
    public float FAST_SONG_SPEED_CAP = 1.0f;
    public float waitTime = WAIT_LIMIT;
    public float crunchTime = 30f;
    // THIS IS DUMMY CODE BELOW
    // THIS WILL BE UPDATED WHEN GAME MANAGER IS IN THE GAME
    public float TIME_DURATION = 300f;
    public float timeRemaining = 150f;
    // END DUMMY CODE
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioMixerGroup = audioSource.outputAudioMixerGroup;
        songSpeed = SLOW_SONG_SPEED_CAP;
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
            // THIS IS DUMMY CODE BELOW
            // THIS WILL BE UPDATED WHEN GAME MANAGER IS IN THE GAME
            timeRemaining -= WAIT_LIMIT;
            // END DUMMY CODE
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
        float slope = FindSlope(TIME_DURATION, crunchTime, SLOW_SONG_SPEED_CAP, FAST_SONG_SPEED_CAP);
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
