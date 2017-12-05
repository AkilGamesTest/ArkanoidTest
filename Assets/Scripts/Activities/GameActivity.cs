using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameActivity : Activity
{
    public Text hpText, scoreText, timeText, numberText;

    ILevels levels;
    ILevels Levels
    {
        get
        {
            if (levels == null)
            {
                levels = ManagersContainer.Instance.LevelsController;
            }
            return levels;
        }
    }

    Coroutine timerCoroutine;

    void Start()
    {
        Levels.onHpChange += hp =>
        {
            hpText.text = hp.ToString();
        };
        Levels.onScoreChange += score =>
        {
            scoreText.text = score.ToString();
        };
        Levels.onTimeChange += time =>
        {
            timeText.text = $"{(time / 60) % 60:d2}:{time % 60:d2}";
        };
    }

    public override void Hide()
    {
        Sound.PlaySound("Click");
        Activities[ActivityName.CLOSE_GAME].Show();
        Levels.IsPaused = true;
        StopCoroutine(timerCoroutine);
    }

    public override void Show()
    {
        base.Show();
        numberText.text = $"Level {Levels.LevelNumber:d2}";
        Levels.IsPaused = false;
        timerCoroutine = StartCoroutine(Timer());
    }

    public void OnResetClick()
    {
        Sound.PlaySound("Click");
        Levels.RestartLevel();
    }

    IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            Levels.ElapcedTime++;
            timeText.text = $"{(Levels.ElapcedTime / 60) % 60:d2}:{Levels.ElapcedTime % 60:d2}";
        }
    }
}