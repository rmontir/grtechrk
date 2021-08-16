using System.Text.RegularExpressions;

namespace GrTechRK.BSL.Common
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                    + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                regex.IsMatch(email);
            }

            return false;
        }
    }
}
