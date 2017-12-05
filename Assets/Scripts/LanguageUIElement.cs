using UnityEngine;
using UnityEngine.UI;

public class LanguageUIElement : MonoBehaviour
{
    public string stringName;

    ILanguage language;
    ILanguage Language
    {
        get
        {
            if (language == null)
            {
                language = ManagersContainer.Instance.LanguageController;
            }
            return language;
        }
    }

    Text text;

    void Start()
    {
        text = GetComponent<Text>();

        SetLanguage();
        Language.onLanguageChanged += SetLanguage;
    }

    public void SetLanguage()
    {
        text.text = Language.GetString(stringName);
    }
}