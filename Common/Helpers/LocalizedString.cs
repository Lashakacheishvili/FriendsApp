using Common.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Helpers
{
    public static class LocalizedString
    {
        public static string GetRsValidatorTranslation(this string validationString)
        {
            string lang = RequestContextManager.Instance.CurrentContext.Request.Headers["accept-language"].ToString().Split('-')[0].ToString();
            var culture = new CultureInfo("en-US");
            if (lang.Equals("ka"))
                culture = new CultureInfo("ka-GE");
            return RsStrings.ResourceManager.GetString(validationString, culture);
        }
        public static string GetTextTranslation(this string textGe, string textEn, string textRu)
        {
            string lang = RequestContextManager.Instance.CurrentContext.Request.Headers["accept-language"].ToString().Split('-')[0].ToString();
            return string.IsNullOrEmpty(lang) ? textEn : lang.ToLower().Equals("ka") ? textGe : lang.ToLower().Equals("en") ? textEn : textRu;
        }
    }
}
