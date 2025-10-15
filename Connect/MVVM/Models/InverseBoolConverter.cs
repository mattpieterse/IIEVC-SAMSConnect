using System.Globalization;
using System.Windows.Data;

namespace SAMS.Connect.MVVM.Models;

public sealed class InverseBoolConverter
    : IValueConverter
{
#region Converters

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        (value is bool b ? !b : value) ?? throw new InvalidOperationException();


    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        (value is bool b ? !b : value) ?? throw new InvalidOperationException();

#endregion
}
