using UnityEngine;

public class LoseActivity : Activity
{
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

    public override void Show()
    {
        group.alpha = 1.0f;
        gameObject.SetActive(true);
        Activities.ActivitiesStack.Push(this);
    }

    public void OnYesClick()
    {
        Sound.PlaySound("Click");
        Levels.RestartLevel();
        Hide();
    }

    public void OnNoClick()
    {
        Sound.PlaySound("Click");
        Levels.DestroyLevel();
        Activities[ActivityName.MAIN].Show();
    }
}