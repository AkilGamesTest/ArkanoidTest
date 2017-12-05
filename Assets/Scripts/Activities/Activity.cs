using UnityEngine;

public class Activity : MonoBehaviour
{
    [HideInInspector]
    public CanvasGroup group;

    IActivities activities;
    public IActivities Activities
    {
        get
        {
            if(activities == null)
            {
                activities = ManagersContainer.Instance.ActivityController;
            }
            return activities;
        }
    }

    ISound sound;
    protected ISound Sound
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

    void Awake()
    {
        group = GetComponent<CanvasGroup>();
        OnAwake();
    }

    protected virtual void OnAwake() { }

    public virtual void Show()
    {
        if (Activities.ActivitiesStack.Count > 0)
        {
            Activities.ActivitiesStack.Peek().gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        group.alpha = 1.0f;
        Activities.ActivitiesStack.Push(this);
    }

    public virtual void Hide()
    {
        group.alpha = 0.0f;
        gameObject.SetActive(false);
        Activities.ActivitiesStack.Pop();
        if(Activities.ActivitiesStack.Count > 0)
        {
            Activities.ActivitiesStack.Peek().Show();
        }
    }
}