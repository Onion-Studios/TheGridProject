using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    private AudioSource source;
    public string clipName;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0f, 2f)]
    public float pitch;
    public bool loop;
    public bool playOnAwake;
    public AudioMixerGroup mixer;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = this.clip;
        source.volume = this.volume;
        source.pitch = this.pitch;
        source.loop = this.loop;
        source.playOnAwake = this.playOnAwake;
        source.outputAudioMixerGroup = this.mixer;
    }

    public void PlayAudio()
    {
        source.Play();
    }
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField]
    private Sound[] sounds;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObj = new GameObject("Sound_" + i + sounds[i].clipName);
            soundObj.transform.SetParent(this.transform);
            sounds[i].SetSource(soundObj.AddComponent<AudioSource>());
        }
        PlaySound("MainTrack");
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if ( s == null)
        {
            Debug.LogError("Sound: " + name + " NotFound");
            return;
        }
        s.PlayAudio();
    }
}
