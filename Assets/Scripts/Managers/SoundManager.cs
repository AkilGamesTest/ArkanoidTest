using UnityEngine;

public class SoundManager : Singleton<SoundManager>, ISound
{
    AudioSource music;
    AudioClip[] soundsCache;

    public bool IsMusicOn
    {
        get
        {
            return !music.mute;
        }
        set
        {
            music.mute = !value;
            PlayerPrefs.SetInt("IsMusicOn", value ? 1 : -1);
        }
    }

    public float MusicVolume
    {
        get
        {
            return music.volume;
        }
        set
        {
            music.volume = value;
        }
    }
    
    public float SoundVolume { get; set; }
    
    protected override void OnAwake()
    {
        soundsCache = Resources.LoadAll<AudioClip>("Sounds");
        music = gameObject.AddComponent<AudioSource>();
        music.loop = true;
        music.clip = FindByName("Background");
        music.mute = PlayerPrefs.GetInt("IsMusicOn", 1) == -1;
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        music.Play();
    }
    
    public void PlaySound(string name)
    {
        AudioClip sound = FindByName(name);
        if(sound == null)
        {
            return;
        }
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, SoundVolume);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
    }

    AudioClip FindByName(string name)
    {
        for (int i = 0; i < soundsCache.Length; i++)
            if ((soundsCache[i]).name.Equals(name))
                return soundsCache[i];
        return null;
    }
}