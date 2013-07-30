using System;

namespace Utils
{
    public static class NullableUtils
    {
        public static bool IsNotValuedWith<T>(this T? value, Func<T, bool> func) where T : struct
        {
            return !(value.HasValue && func(value.Value));
        }
    }
}
