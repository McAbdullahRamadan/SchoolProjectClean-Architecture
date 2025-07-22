using System.Globalization;

namespace Data.Commons
{
    public class GenralLocalizableEntities
    {
        public string Localized(string textAr, string textEn)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textAr;
            return textEn;

        }
    }
}
