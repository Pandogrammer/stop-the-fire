using System;

namespace Utils.Extensions
{
    public static class IntExtensions
    {
        public static bool ToBool(this int n)
        {
            if (n == 0)
                return false;
            if (n == 1)
                return true;
            throw new Exception("Must be 0 or 1.");
        }

        public static int ToInt(this bool b)
        {
            if (b)
                return 1;
            return 0;
        }
    }
}