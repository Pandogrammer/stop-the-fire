using System;

namespace Utils.Extensions
{
    public static class BoolExtensions
    {
        public static bool DoWhenTrue(this bool b, Action f)
        {
            if (b)
                f();
            return b;
        }
        
        public static bool DoWhenFalse(this bool b, Action f)
        {
            if (!b)
                f();
            return b;
        }
    }
}