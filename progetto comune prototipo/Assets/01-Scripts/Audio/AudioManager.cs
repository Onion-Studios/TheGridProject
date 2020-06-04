using System;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// A class to store and manage all sound assets and their settings
/// </summary>
[System.Serializable]
public class Sound
{
    private AudioSource source;
    public string clipName;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 2f)]
    public float pitch;
    public bool loop;
    public bool playOnAwake;
    public AudioMixerGroup mixer;

    /// <summary>
    /// Set the audiosource and his settings
    /// </summary>
    /// <param name="_source">The source of the audio</param>
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
    /// <summary>
    /// Plays the audio source
    /// </summary>
    public void PlayAudio()
    {
        source.Play();
    }
    /// <summary>
    /// Stops the audio source
    /// </summary>
    public void StopAudio()
    {
        source.Stop();
    }
    /// <summary>
    /// Set the looping options of the audio source
    /// </summary>
    /// <param name="loop">True or false</param>
    public void SetLoop(bool loop)
    {
        source.loop = loop;
    }
    /// <summary>
    /// Check if the source is playing
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return source.isPlaying;
    }


}

/// <summary>
/// A class to instantiate and manage alla the sounds in the game
/// </summary>
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
        PlaySound("MenuTheme");
    }
    /// <summary>
    /// Plays the respective sound
    /// </summary>
    /// <param name="name">The sound name</param>
    public void PlaySound(string name)
    {
        SearchSound(name).PlayAudio();
    }
    /// <summary>
    /// Stops the respective sound
    /// </summary>
    /// <param name="name">The sound name</param>
    public void StopSound(string name)
    {
        SearchSound(name).StopAudio();
    }
    /// <summary>
    /// Set the loop option of the resepctive sound
    /// </summary>
    /// <param name="name">The sound name</param>
    /// <param name="loop">True or false</param>
    public void SetLoop(string name, bool loop)
    {
        SearchSound(name).SetLoop(loop);
    }
    /// <summary>
    /// Check if the respective sound is playing
    /// </summary>
    /// <param name="name">The souond name</param>
    /// <returns>True if the sound is playing, false if it's not</returns>
    public bool IsPlaying(string name)
    {
        return SearchSound(name).IsPlaying();
    }
    /// <summary>
    /// Search the sound file in the sounds array
    /// </summary>
    /// <param name="name">The sound name</param>
    /// <returns>The sound asset reference</returns>
    private Sound SearchSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " NotFound");
            return null;
        }
        return s;
    }
    /// <summary>
    /// Stops all the sounds in the game
    /// </summary>
    public void StopAllSounds()
    {
        foreach (Sound s in sounds)
        {
            s.StopAudio();
        }
    }
}
