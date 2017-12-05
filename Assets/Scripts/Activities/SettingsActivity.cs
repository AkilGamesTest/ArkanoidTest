using UnityEngine;
using UnityEngine.UI;

public class SettingsActivity : Activity
{
    public Slider soundSlider, musicSlider;
    public Image languageButton;
    public Sprite[] languages;

    ILanguage language;
    ILanguage Language
    {
        get
        {
            if (language == null)
            {
                language = ManagersContainer.Instance.LanguageController;
            }
            return language;
        }
    }

    public override void Show()
    {
        base.Show();
        soundSlider.value = Sound.SoundVolume;
        musicSlider.value = Sound.MusicVolume;
        languageButton.sprite = languages[(int)Language.CurrentLanguage];
    }

    public override void Hide()
    {
        group.alpha = 0.0f;
        Sound.PlaySound("Click");
        Activities[global::ActivityName.MAIN].Show();
        Sound.SaveSettings();
    }

    public void SetSound()
    {
        Sound.SoundVolume = soundSlider.value;
    }

    public void SetMusic()
    {
        Sound.MusicVolume = musicSlider.value;
    }

    public void ChangeLanguage()
    {
        Sound.PlaySound("Click");
        LANGUAGE lang = Language.CurrentLanguage;
        if(lang++ == LANGUAGE.RUS)
        {
            lang = LANGUAGE.ENG;
        }
        languageButton.sprite = languages[(int)lang];
        Language.CurrentLanguage = lang;
    }
}