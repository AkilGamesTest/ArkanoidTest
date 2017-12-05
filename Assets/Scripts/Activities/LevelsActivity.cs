using UnityEngine;

public class LevelsActivity : Activity
{
    public override void Hide()
    {
        group.alpha = 0.0f;
        Activities[ActivityName.MAIN].Show();
    }

    protected override void OnAwake ()
    {
        PlayerPrefs.SetInt($"Level01", 1);
    }
}