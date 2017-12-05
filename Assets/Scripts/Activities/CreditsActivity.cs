using UnityEngine;

public class CreditsActivity : Activity
{
    public override void Hide()
    {
        group.alpha = 0.0f;
        Activities[ActivityName.MAIN].Show();
    }
}