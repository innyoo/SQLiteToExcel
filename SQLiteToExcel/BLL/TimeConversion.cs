using System;

namespace SQLiteToExcel.BLL
{
    public class TimeConversion   //时间换算
    {
        public static int _correctionValue = 8 * 60 * 60;
        public static string TimeStamp_DataTime(long unixTimeStamp)   //时间戳到日期时间
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(unixTimeStamp - _correctionValue);
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string TimeStamp_Data(long unixTimeStamp)   //时间戳到日期
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(unixTimeStamp - _correctionValue);
            return dt.ToString("yyyy-MM-dd");
        }

        public static string TimeStamp_Time(long unixTimeStamp)   //时间戳到时间
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(unixTimeStamp - _correctionValue);
            return dt.ToString("HH:mm:ss");
        }

        public static long DataTime_TimeStamp(DateTime dt)     //时间到时间戳
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(dt - startTime).TotalSeconds; // 相差秒数
            return timeStamp + _correctionValue;
        }

    }
}
