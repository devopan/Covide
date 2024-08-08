using Covide.Web.Domain.Models;
using System;
using System.Globalization;

namespace Covide.Web.Domain
{
    public class ConversionService : IConversionService
    {
        public ConversionModel ComputeConversionModel(string hex)
        {
            var red = int.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            var green = int.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            var blue = int.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            var redPercentage = Math.Round(red / 2.55, 1);
            var greenPercentage = Math.Round(green / 2.55, 1);
            var bluePercentage = Math.Round(blue / 2.55, 1);

            var redNormalized = (double)red / 255;
            var greenNormalized = (double)green / 255;
            var blueNormalized = (double)blue / 255;

            var key = 1 - Math.Max(redNormalized, Math.Max(greenNormalized, blueNormalized));
            var cyan = (1 - redNormalized - key) / (1 - key);
            var magenta = (1 - greenNormalized - key) / (1 - key);
            var yellow = (1 - blueNormalized - key) / (1 - key);

            var keyPercentage = (int)Math.Round(key * 100);
            var cyanPercentage = (int)Math.Round(cyan * 100);
            var magentaPercentage = (int)Math.Round(magenta * 100);
            var yellowPercentage = (int)Math.Round(yellow * 100);

            var cmax = Math.Max(redNormalized, Math.Max(greenNormalized, blueNormalized));
            var cmin = Math.Min(redNormalized, Math.Min(greenNormalized, blueNormalized));
            var delta = cmax - cmin;

            var lightness = (cmax + cmin) / 2;

            var hue = delta == 0 ? 0
                : cmax == redNormalized ? (greenNormalized - blueNormalized) / delta % 6
                : cmax == greenNormalized ? (blueNormalized - redNormalized) / delta + 2
                : (redNormalized - greenNormalized) / delta + 4;
            hue *= 60;
            hue = hue < 0 ? hue += 360 : hue;
            hue = Math.Round(hue);

            var saturation = delta == 0 ? 0
                : delta / (1 - Math.Abs(2 * lightness - 1));
            saturation *= 100;
            saturation = Math.Round(saturation, 1);

            lightness *= 100;
            lightness = Math.Round(lightness, 1);

            var value = cmax * 100;
            value = Math.Round(value, 1);

            double[] xyz = ComputeXyz(redNormalized, greenNormalized, blueNormalized);

            var x = Math.Round(xyz[0], 3);
            var y = Math.Round(xyz[1], 3);
            var z = Math.Round(xyz[2], 3);

            return new ConversionModel()
            {
                RgbDecimal = new[] { red, green, blue },
                RgbPercentage = new[] { redPercentage, greenPercentage, bluePercentage },
                Cmyk = new[] { cyanPercentage, magentaPercentage, yellowPercentage, keyPercentage },
                Hsl = new[] { hue, saturation, lightness },
                Hsv = new[] { hue, saturation, value },
                Xyz = new[] { x, y, z }
            };
        }
        private static double[] ComputeXyz(double r, double g, double b)
        {
            if (r > 0.04045)
            {
                r = Math.Pow((r + 0.055) / 1.055, 2.4);
            }
            else
            {
                r = r / 12.92;
            }

            if (g > 0.04045)
            {
                g = Math.Pow((g + 0.055) / 1.055, 2.4);
            }
            else
            {
                g = g / 12.92;
            }

            if (b > 0.04045)
            {
                b = Math.Pow((b + 0.055) / 1.055, 2.4);
            }
            else
            {
                b = b / 12.92;
            }

            r *= 100;
            g *= 100;
            b *= 100;

            double x, y, z;

            x = r * 0.4124 + g * 0.3576 + b * 0.1805;
            y = r * 0.2126 + g * 0.7152 + b * 0.0722;
            z = r * 0.0193 + g * 0.1192 + b * 0.9505;

            return new[] { x, y, z };
        }
    }
}
