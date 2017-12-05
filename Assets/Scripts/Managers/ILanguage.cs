public interface ILanguage
{
    event System.Action onLanguageChanged;
    
    LANGUAGE CurrentLanguage { get; set; }

    string GetString(string name);
}