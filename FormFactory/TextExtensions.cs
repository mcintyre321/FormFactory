using System.Text;

namespace FormFactory
{
    public static class TextExtensions
    {
        public static string Sentencise(this string str, bool titlecase = false)
        {
            if (str == null || str.Length == 0)
                return null;

            StringBuilder retVal = new StringBuilder(32);

            retVal.Append(char.ToUpper(str[0]));
            for (int i = 1; i < str.Length; i++)
            {
                if (char.IsLower(str[i]))
                {
                    retVal.Append(str[i]);
                }
                else
                {
                    retVal.Append(" ");
                    if (titlecase)
                    {
                        retVal.Append(char.ToUpper(str[i]));
                    }else
                    {
                        retVal.Append(char.ToLower(str[i]));
                    }
                }
            }

            return retVal.ToString();
        }
    }
}