using System.Collections.Generic;


public class LanguageStringsCollection : Dictionary<LANGUAGE, Dictionary<string, string>>
{
    public static LanguageStringsCollection LoadFromXml(string name)
    {
        //change
        return BuildLanguageStrings();
    }

    public static LanguageStringsCollection BuildLanguageStrings()
    {
        var res = new LanguageStringsCollection();
        var strings = new Dictionary<LANGUAGE, Dictionary<string, string>>()
        {
            { 
                #region Eng 
                LANGUAGE.ENG, new Dictionary<string, string>()
                { 
                    #region Main Menu 
                    { "Play", "Play" },
                    { "Settings", "Settings" },
                    { "Credits", "Credits" },
                    #endregion 

                    #region About Menu 
                    {"AboutText", "Game By Akil games" +
                    "\n\n" +
                    "Game design:\n"+ "Alexander Kalinichenko\n\n" +
                    "Programming: \n" +
                    "Alexander Kalinichenko\n\n" +
                    "Design & Art: \n"+
                    "Alexander Kalinichenko\n\n" +
                    "Sound & Music: \n" +
                    "Freesound.org \n\n" +
                    "Testers: \n"+
                    "Some friends :)\n\n"},
                    #endregion 

                    #region Settings Menu 
                    { "Sound", "Sound:" },
                    { "Music", "Music:" },
                    { "Language", "Language:" },
                    #endregion

                    #region Game Panel 
                    { "Level", "Level" },
                    #endregion 

                    #region Lose Panel 
                    { "Lose", "You lose. Restart?" },
                    #endregion 

                    #region Win Panel 
                    { "Win", "You win. Next?"},
                    #endregion 

                    #region Win Panel 
                    { "End", "It was the last level. Folow the updates."},
                    { "Ok", "Ok"},
                    #endregion 
                    
                    #region Close Panel 
                    { "Exit", "Exit?" },
                    { "Yes", "Yes" },
                    { "No", "No" },
                    #endregion 
                }    
                #endregion 
            },
            { 
                #region Rus 
                LANGUAGE.RUS, new Dictionary<string, string>()
                { 
                    #region Main Menu 
                    { "Play", "Играть" },
                    { "Settings", "Настройки" },
                    { "Credits", "Авторы" },
                    #endregion 

                    #region About Menu 
                    {"AboutText", "Игра от Akil games" +
                    "\n\n" +
                    "Игровой дизайн:\n"+ "Александр Калиниченко\n\n" +
                    "Программирование: \n" +
                    "Александр Калиниченко \n\n" +
                    "Дизайн: \n"+
                    "Александр Калиниченко \n\n" +
                    "Звук и музыка: \n" +
                    "Freesound.org \n\n" +
                    "Тестирование: \n"+
                    "Друзья :)\n\n"},
                    #endregion 

                    #region Settings Menu 
                    { "Sound", "Звук:" },
                    { "Music", "Музыка:" },
                    { "Language", "Язык:" },
                    #endregion 

                    #region Game Panel 
                    { "Level", "Уровень" },
                    #endregion 

                    #region Lose Panel 
                    { "Lose", "Вы проиграли. Начать заново?" },
                    #endregion 

                    #region Win Panel 
                    { "Win", "Вы выиграли. Дальше?"},
                    #endregion 

                    #region Win Panel 
                    { "End", "Это был последний уровень. Следите за обновлениями."},
                    { "Ok", "Ok"},
                    #endregion 

                    #region Close Panel 
                    { "Exit", "Выйти?" },
                    { "Yes", "Да" },
                    { "No", "Нет" },
                    #endregion 
                }    
                #endregion 
            }
        };
        foreach (var pair in strings)
        {
            res.Add(pair.Key, pair.Value);
        }
        return res;
    }
}