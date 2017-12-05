public interface ISound
{
    bool IsMusicOn { get; set; }

    float MusicVolume { get; set; }

    float SoundVolume { get; set; }
    
    void PlaySound(string name);

    void SaveSettings();
}