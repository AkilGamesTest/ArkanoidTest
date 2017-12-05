using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainActivity : Activity
{
    public Image blackHover;

    public void OnPlayClick()
    {
        Sound.PlaySound("Click");
        Activities[ActivityName.LEVELS].Show();
    }

    public void OnSettingsClick()
    {
        Sound.PlaySound("Click");
        Activities[ActivityName.SETTINGS].Show();
    }

    public void OnCreditsClick()
    {
        Sound.PlaySound("Click");
        Activities[ActivityName.CREDITS].Show();
    }

    public override void Show()
    {
        if(Activities.ActivitiesStack.Count == 0)
        {
            Activities.ActivitiesStack.Push(this);
        }
        else
        {
            Activity a;
            while(Activities.ActivitiesStack.Count > 1)
            {
                a = Activities.ActivitiesStack.Pop();
                a.group.alpha = 0.0f;
                a.gameObject.SetActive(false);
            }
        }
        group.alpha = 1.0f;
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        blackHover.gameObject.SetActive(true);
        StartCoroutine(FadeWindow());
    }

    public IEnumerator FadeWindow()
    {
        Color c = blackHover.color;

        // DOTween, i miss you... ;(
        for (float i = 0; i <= 1.0f; i += 0.1f) 
        {
            c.a = i;
            blackHover.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        blackHover.color = Color.black;
        Application.Quit();
        yield return null;
    }
}