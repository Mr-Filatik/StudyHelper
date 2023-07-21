using Google.Protobuf.WellKnownTypes;

namespace StudyHelper.Application.Server.Other
{
    public static class Converters
    {
        //public static DateTime ToDateTime(Timestamp time)
        //{
        //    return Timestamp.FromDateTime(DateTime.SpecifyKind(time, DateTimeKind.Utc));
        //}

        public static Timestamp ToTimestamp(DateTime? dateTime)
        {
            if (dateTime == null || !dateTime.HasValue) return new Timestamp();
            return Timestamp.FromDateTime(DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc));
        }
    }
}
