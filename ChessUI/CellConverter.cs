using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using chesslib.Command;

namespace ChessUI
{
    public class CellConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] letters = new[] { "a", "b", "c","d", "e", "f", "g", "h" };
            int[] numbers = new[] {1, 2, 3, 4, 5, 6, 7, 8};
            var moveCommand = (MakeMoveCommand) value;
            return string.Format("{0}{1} - {2}{3}", letters[moveCommand.PrevY], numbers[moveCommand.PrevX],
                letters[moveCommand.NextY], numbers[moveCommand.NextX]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}