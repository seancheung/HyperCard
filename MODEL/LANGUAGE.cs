
namespace MODEL
{
    /// <summary>
    /// Available languages of the card info
    /// </summary>
    public enum LANGUAGE
    {
        Chinese_Simplified,
        Chinese_Traditional,
        German,
        French,
        Italian,
        Japanese,
        Korean,
        Portuguese,
        Russian,
        Spanish,
        English
    }

    public class Language
    {
        public static string GetLangCode(LANGUAGE lang)
        {
            string result = "en";

            switch (lang)
            {
                case LANGUAGE.Chinese_Simplified:
                    result = "cn";
                    break;
                case LANGUAGE.Chinese_Traditional:
                    result = "tw";
                    break;
                case LANGUAGE.German:
                    result = "ge";
                    break;
                case LANGUAGE.French:
                    result = "fr";
                    break;
                case LANGUAGE.Italian:
                    result = "it";
                    break;
                case LANGUAGE.Japanese:
                    result = "jp";
                    break;
                case LANGUAGE.Korean:
                    result = "ko";
                    break;
                case LANGUAGE.Portuguese:
                    result = "pt";
                    break;
                case LANGUAGE.Russian:
                    result = "ru";
                    break;
                case LANGUAGE.Spanish:
                    result = "sp";
                    break;
                case LANGUAGE.English:
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
