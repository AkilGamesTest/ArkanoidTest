using UnityEngine;

public enum LANGUAGE : byte
{
    ENG,
    RUS
}

public class LanguageManager : Singleton<LanguageManager>, ILanguage
{
    public event System.Action onLanguageChanged;

    LANGUAGE language;
    public LANGUAGE CurrentLanguage
    {
        get
        {
            return language;
        }
        set
        {
            if(value != language)
            {
                language = value;
                onLanguageChanged?.Invoke();
                PlayerPrefs.SetInt("Language", (int)value);
            }
        }
    }

    protected override void OnAwake()
    {
        collection = LanguageStringsCollection.BuildLanguageStrings();
        language = (LANGUAGE)PlayerPrefs.GetInt("Language", 0);
    }

    LanguageStringsCollection collection = null;

    public string GetString(string name)
    {
        return collection[language].ContainsKey(name) ? collection[language][name] : "";
    }
}