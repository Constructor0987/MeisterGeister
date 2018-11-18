using System;
using MeisterGeister.ViewModel.Kampf.Logic;
using System.Windows.Data;
using System.Globalization;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class PositionToImageConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new PositionToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Ressources.GetRelativeApplicationPathForImagesIcons() + "StandingCreature.png";

            if (((Position)value) == Position.Liegend)
                return Ressources.GetRelativeApplicationPathForImagesIcons() + "OnTheGroundCreature.png";
            if (((Position)value) == Position.Kniend)
                return Ressources.GetRelativeApplicationPathForImagesIcons() + "KneelingCreature.png";
            if (((Position)value) == Position.Stehend)
                return Ressources.GetRelativeApplicationPathForImagesIcons() + "StandingCreature.png";
            if (((Position)value) == Position.Schwebend)
                return Ressources.GetRelativeApplicationPathForImagesIcons() + "FloatingCreature.png";
            if (((Position)value) == Position.Reitend)
                return Ressources.GetRelativeApplicationPathForImagesIcons() + "RidingCreature.png";
            if (((Position)value) == Position.Fliegend)
                return Ressources.GetRelativeApplicationPathForImagesIcons() + "FlyingCreature.png";

            return Ressources.GetRelativeApplicationPathForImagesIcons() + "StandingCreature.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
