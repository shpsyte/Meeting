using System;

namespace Business.Extensions {
    public static class DateTimeExtensions {
        public static string ToSql (this DateTime date) => date.ToString ("yyyy-MM-dd");

    }
}