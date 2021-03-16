using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Toggle BGmusic, Btnmusic;
    // public AudioClip BgMusic;
    public AudioSource BGmusicSource;
    public AudioSource ChipsSource;

    public AudioClip AddChipsClip;
    public AudioClip RemoveChipsClip;
    public AudioClip SwitchOnClip;
    public AudioClip SwitchOffClip;
    public AudioClip DiceRollClip;
    public AudioClip ButtonClickClip;
    public AudioClip ChipsSelectionClip;
    public AudioClip[] BasicClips;
    public AudioClip[] FullOffClips;
    public AudioClip[] FullOnClips;
    public static event Action<bool> OnSoundStatusChangedEvent;
    public static event Action<bool> OnMusicStatusChangedEvent;

    [HideInInspector] public bool isSoundEnabled = true;
    [HideInInspector] public bool isMusicEnabled = true;

    private static SoundManager _instance;
    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null)
        {
            if (_instance.gameObject != gameObject)
            {
                Destroy(gameObject);
                return;
            }
        }
        _instance = FindObjectOfType<SoundManager>();
        OnMusicStatusChangedEvent += OnMusicStatusChanged;
        OnSoundStatusChangedEvent += OnSoundStatusChangedEvent;
        ChipsSource.volume = 0;
        //    Invoke("StartBGMusic", 0.2F);
    }

    void OnEnable()
    {
        Scene c  = SceneManager.GetActiveScene();
        if (c.buildIndex ==1 || c.buildIndex == 2)
            initAudioStatus();
    }

    public void initAudioStatus()
    {
        isSoundEnabled = (PlayerPrefs.GetInt("isSoundEnabled", 0) == 0) ? true : false;
        isMusicEnabled = (PlayerPrefs.GetInt("isMusicEnabled", 0) == 0) ? true : false;


        if (isMusicEnabled)
        {
            BGmusic.isOn = true;
            BGmusic.GetComponent<UIAddons.CustomToggle>().OnValueChanged(true);
        }
        else
        {
            BGmusic.isOn = false; BGmusic.GetComponent<UIAddons.CustomToggle>().OnValueChanged(false);
        }

        if (isSoundEnabled)
        {
            Btnmusic.isOn = true;
            Btnmusic.GetComponent<UIAddons.CustomToggle>().OnValueChanged(true);
        }
        else
        {
            Btnmusic.isOn = false;
            Btnmusic.GetComponent<UIAddons.CustomToggle>().OnValueChanged(false);
        }

        if ((!isSoundEnabled) && (OnSoundStatusChangedEvent != null))
        {
            OnSoundStatusChangedEvent.Invoke(isSoundEnabled);
        }
        if ((!isMusicEnabled) && (OnMusicStatusChangedEvent != null))
        {
            OnMusicStatusChangedEvent.Invoke(isMusicEnabled);
        }

        BGmusic.onValueChanged.AddListener((arg0) => ToggleMusicStatus());
        Btnmusic.onValueChanged.AddListener((arg0) => ToggleSoundStatus());
    }

    public void ToggleSoundStatus()
    {
        isSoundEnabled = (isSoundEnabled) ? false : true;
        PlayerPrefs.SetInt("isSoundEnabled", (isSoundEnabled) ? 0 : 1);

        if (OnSoundStatusChangedEvent != null)
        {
            Debug.Log("ToggleSoundStatus 11 ");
            OnSoundStatusChangedEvent.Invoke(isSoundEnabled);
        }
    }

    public void ToggleMusicStatus()
    {
        isMusicEnabled = (isMusicEnabled) ? false : true;
        PlayerPrefs.SetInt("isMusicEnabled", (isMusicEnabled) ? 0 : 1);

        if (OnMusicStatusChangedEvent != null)
        {
            OnMusicStatusChangedEvent.Invoke(isMusicEnabled);
        }
    }

    public void StartBGMusic()
    {
        if (isMusicEnabled)
        {
            if (!BGmusicSource.isPlaying)
            {
                BGmusicSource.Play();
            }
        }
    }
    /// <summary>
    /// Stops the background music.
    /// </summary>
    public void StopBGMusic()
    {
        BGmusicSource.Stop();
    }


    /// <summary>
    /// Raises the disable event.
    /// </summary>
    void OnDisable()
    {
        OnMusicStatusChangedEvent -= OnMusicStatusChanged;
        OnSoundStatusChangedEvent -= OnSoundStatusChanged;

    }

    /// <summary>
    /// Raises the music status changed event.
    /// </summary>
    /// <param name="isSoundEnabled">If set to <c>true</c> is sound enabled.</param>
    void OnMusicStatusChanged(bool isSoundEnabled)
    {
        if (isSoundEnabled)
        {
            StartBGMusic();
        }
        else
        {
            StopBGMusic();
        }
    }

    void OnSoundStatusChanged(bool isSoundEnabled)
    {
        if (isSoundEnabled)
        {

            if (!ChipsSource.isPlaying)
            {
                ChipsSource.Play();
            }

        }
        else
        {
            ChipsSource.Stop();
        }
    }
    public  void playForOneShot(AudioClip passClip)
    {
        if (isSoundEnabled)
        {
            ChipsSource.PlayOneShot(passClip);
        }
    }
}
