using UnityEngine;

public class EndGameActivity : Activity
{
    public override void Show()
    {
        group.alpha = 1.0f;
        gameObject.SetActive(true);
        Activities.ActivitiesStack.Push(this);
    }

    public override void Hide()
    {
        Sound.PlaySound("Click");
        Activities[ActivityName.MAIN].Show();
    }
}