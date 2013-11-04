using System;

namespace HappyBetter.Common
{
    public static class TextStateUtil
    {
        public static Boolean IsCompleted(String textString)
        {
            if (String.IsNullOrWhiteSpace(textString))
            {
                return false;
            }

            return textString.Length > 5;
        }
    }
}
