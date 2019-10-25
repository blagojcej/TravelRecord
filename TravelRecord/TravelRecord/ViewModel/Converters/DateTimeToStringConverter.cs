using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TravelRecord.ViewModel.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset dateTime = (DateTimeOffset) value;
            DateTimeOffset rightNow=DateTimeOffset.Now;
            var difference = rightNow - dateTime;

            if (difference.TotalDays > 1)
                return $"{dateTime:d}";
            else
            {
                // Less than minute has pass
                if (difference.TotalSeconds < 60)
                    return $"{difference.TotalSeconds:0} seconds ago";

                // Less than hour has pass
                if (difference.TotalMinutes < 60)
                    return $"{difference.TotalMinutes:0} minutes ago";

                //Less than day has pass
                if (difference.TotalHours < 24)
                    return $"{difference.TotalHours:0} hours ago";

                return "yesterday";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTimeOffset.Now;
        }
    }
}
