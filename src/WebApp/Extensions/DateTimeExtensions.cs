using System;

namespace WebApp.Extensions {
    public static class DateTimeExtensions {
        public static string ToSQL (this DateTime time) {
            return time.ToString ("yyyy-MM-dd");

        }

    }
}