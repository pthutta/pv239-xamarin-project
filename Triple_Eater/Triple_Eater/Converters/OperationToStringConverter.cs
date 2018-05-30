using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Triple_Eater.DataModels;
using Xamarin.Forms;

namespace Triple_Eater.Converters
{
    public class OperationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var operation = (Operation) value;
            switch (operation)
            {
                case Operation.Confession:
                    return "Confession";
 
                case Operation.SecretIntel:
                    return "Secret Intel";

                case Operation.AnonymousTip:
                    return "Anonymous Tip";

                case Operation.NeighborhoodGossip:
                    return "Neighborhood Gossip";

                case Operation.NightPhotographs:
                    return "Night Photographs";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
