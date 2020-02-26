using System;
using System.Globalization;
using System.Windows.Data;

namespace RentCar.UI.ValueConverters
{
  /// <summary>
  /// Nullable support decimal<->string value converter.
  /// </summary>
  public class DecimalToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        return null;

      return System.Convert.ToDecimal(value).ToString(parameter as string);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (decimal.TryParse(value?.ToString(), out var d))
        return d;

      if (Nullable.GetUnderlyingType(targetType) != null)
        return null;
      else
        return 0m;
    }
  }
}
