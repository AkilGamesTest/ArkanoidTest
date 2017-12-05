using System.Collections.Generic;
using UnityEngine;

public enum ActivityName : byte
{
    MAIN,
    LEVELS,
    SETTINGS,
    CREDITS,
    GAME,
    CLOSE_GAME,
    LOSE,
    WIN,
    END_GAME
}

public class ActivityManager : Singleton<ActivityManager>, IActivities
{
    public Activity[] activities;

    bool isQiuining;

    public Activity this[ActivityName state]
    {
        get
        {
            return activities[(int)state];
        }
    }
    
    public Stack<Activity> ActivitiesStack { get; private set; }

    public void RunUI()
    {
        ActivitiesStack = new Stack<Activity>();
        activities = new Activity[]
        {
            GameObject.FindObjectOfType<MainActivity>(),
            GameObject.FindObjectOfType<LevelsActivity>(),
            GameObject.FindObjectOfType<SettingsActivity>(),
            GameObject.FindObjectOfType<CreditsActivity>(),
            GameObject.FindObjectOfType<GameActivity>(),
            GameObject.FindObjectOfType<CloseGameActivity>(),
            GameObject.FindObjectOfType<LoseActivity>(),
            GameObject.FindObjectOfType<WinActivity>(),
            GameObject.FindObjectOfType<EndGameActivity>(),
        };
        activities[0].Show();
        for (int i = 1; i < activities.Length; i++)
        {
            if (activities[i] != null)
            {
                activities[i].gameObject.SetActive(false);
            }
        }
    }

    ISound sound;
    ISound Sound
    {
        get
        {
            if (sound == null)
            {
                sound = ManagersContainer.Instance.SoundController;
            }
            return sound;
        }
    }

    void Update()
    {
        if(isQiuining)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (ActivitiesStack.Count == 1)
            {
                isQiuining = true;
            }
            Back();
        }
    }

    public void Back()
    {
        Sound.PlaySound("Click");
        ActivitiesStack.Peek().Hide();
    }
}