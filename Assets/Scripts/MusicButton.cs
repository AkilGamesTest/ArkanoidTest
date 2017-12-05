using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
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

    Toggle t;

    void Start()
    {
        t = GetComponent<Toggle>();
        t.isOn = !Sound.IsMusicOn;
    }

    public void OnClick()
    {
        Sound.PlaySound("Click");
        Sound.IsMusicOn = !t.isOn;
    }
}