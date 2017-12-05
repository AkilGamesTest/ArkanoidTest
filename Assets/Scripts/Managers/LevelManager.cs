using UnityEngine;
using System.Collections;

public class LevelManager : Singleton<LevelManager>, ILevels
{
    public event System.Action<int> onHpChange;
    public event System.Action<int> onScoreChange;
    public event System.Action<int> onTimeChange;

    GameObject currentLevelPrefab, currentLevel;
    Transform locationHolder;

    public const int totalLevels = 3;

    IActivities activities;
    public IActivities Activities
    {
        get
        {
            if (activities == null)
            {
                activities = ManagersContainer.Instance.ActivityController;
            }
            return activities;
        }
    }

    public int LevelNumber { get; private set; }
    
    public bool IsPaused { get; set; }

    int elapcedTime;
    public int ElapcedTime
    {
        get
        {
            return elapcedTime;
        }
        set
        {
            if (elapcedTime == value)
            {
                return;
            }
            elapcedTime = value;
            onTimeChange?.Invoke(elapcedTime);
        }
    }

    int hp;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if(hp == value)
            {
                return;
            }
            hp = value;
            onHpChange?.Invoke(hp);
            if (hp <= 0)
            {
                IsPaused = true;
                Activities[ActivityName.LOSE].Show();
            }
        }
    }
    
    int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (score == value)
            {
                return;
            }
            score = value;
            onScoreChange?.Invoke(score);
        }
    }

    public void StartLevel(int level)
    {
        if(locationHolder == null)
        {
            locationHolder = GameObject.Find("LocationHolder").transform;
        }
        currentLevelPrefab = Resources.Load<GameObject>($"Levels/Level{level:d2}");
        StartCoroutine(CreateLevel());

        LevelNumber = level;
        Score = 0;
        Hp = 3;
        ElapcedTime = 0;
    }

    public void NextLevel()
    {
        DestroyLevel();
        if(LevelNumber == totalLevels)
        {
            Activities[ActivityName.END_GAME].Show();
            return;
        }
        LevelNumber++;
        PlayerPrefs.SetInt($"Level{LevelNumber:d2}", 1);
        ElapcedTime = 0;
        StartLevel(LevelNumber);
    }

    public void RestartLevel()
    {
        GameObject old = currentLevel;
        Destroy(old);

        Score = 0;
        Hp = 3;
        ElapcedTime = 0;

        StartCoroutine(CreateLevel());
    }

    public void DestroyLevel()
    {
        GameObject old = currentLevel;
        Destroy(old);
        currentLevelPrefab = null;
        Resources.UnloadUnusedAssets();
    }

    IEnumerator CreateLevel()
    {
        // To do: Screen fading black and returns
        yield return new WaitForSeconds(0.25f);
        currentLevel = Instantiate(currentLevelPrefab, locationHolder);
        yield return null;
    }
}