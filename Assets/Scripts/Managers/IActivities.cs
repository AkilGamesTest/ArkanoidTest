using System.Collections.Generic;

public interface IActivities
{
    Stack<Activity> ActivitiesStack { get; }

    Activity this[ActivityName state] { get; }

    void RunUI();
}