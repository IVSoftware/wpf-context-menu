using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace wpf_context_menu.Converters
{
    public class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public Visibility VisibilityForFalse { get; set; } = Visibility.Hidden;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Equals(value, true) ? Visibility.Visible : VisibilityForFalse;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Equals(value, Visibility.Visible) ? true : false;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
