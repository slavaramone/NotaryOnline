using System;

namespace SharedLib.Extensions
{
    public static class DateTimeOffsetExtensions
    {
		public static  DateTimeOffset RoundUp(this DateTimeOffset dateTimeOff, TimeSpan d)
		{
			var dt = new DateTime((dateTimeOff.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dateTimeOff.DateTime.Kind);
			var roundedDto = new DateTimeOffset(dt, dateTimeOff.Offset);
			return roundedDto;
		}
	}
}
