using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelsActivity controller;
    public GameObject locked;
    public Text numberText;
    public int number;

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

    void OnEnable()
    {
        if (PlayerPrefs.GetInt($"Level{number:d2}", -1) == 1)
        {
            locked.SetActive(false);
            numberText.gameObject.SetActive(true);
            GetComponent<Button>().interactable = true;
        }
        else
        {
            locked.SetActive(true);
            numberText.gameObject.SetActive(false);
            GetComponent<Button>().interactable = false;
        }
    }

    public void OnClick()
    {
        Sound.PlaySound("Click");
        Levels.StartLevel(number);
        controller.Activities[ActivityName.GAME].Show();
    }
}