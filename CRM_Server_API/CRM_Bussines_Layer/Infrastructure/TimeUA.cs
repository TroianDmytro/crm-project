namespace CRM_Business_Layer.Infrastructure
{
    public class TimeUA
    {
        //Выставляет время по Украине
        public static async Task<DateTime> CurrentTimeAsync()
        {
            return await Task.Run(() =>
            {
                TimeZoneInfo ukrainianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
                DateTime createDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukrainianTimeZone);
                return createDate;
            });
        }
        //public static DateTime CurrentTime()
        //{
        //    TimeZoneInfo ukrainianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
        //    DateTime createDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ukrainianTimeZone);
        //    return createDate;
        //}
    }
}
