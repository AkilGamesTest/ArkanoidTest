using UnityEngine;

public class ManagersContainer : Singleton<ManagersContainer>
{
    ILanguage languageController;
    public ILanguage LanguageController
    {
        get
        {
            if(languageController == null)
            {
                languageController = LanguageManager.Instance;
            }
            return languageController;
        }
    }

    IActivities activityController;
    public IActivities ActivityController
    {
        get
        {
            if (activityController == null)
            {
                activityController = ActivityManager.Instance;
            }
            return activityController;
        }
    }

    ISound soundController;
    public ISound SoundController
    {
        get
        {
            if (soundController == null)
            {
                soundController = SoundManager.Instance;
            }
            return soundController;
        }
    }

    ILevels levelsController;
    public ILevels LevelsController
    {
        get
        {
            if (levelsController == null)
            {
                levelsController = LevelManager.Instance;
            }
            return levelsController;
        }
    }

    protected override void OnAwake()
    {
        ActivityController.RunUI();
    }
}