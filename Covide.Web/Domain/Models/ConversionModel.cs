namespace Covide.Web.Domain.Models
{
    public class ConversionModel
    {
        public int[] RgbDecimal { get; set; }
        public double[] RgbPercentage { get; set; }
        public int[] Cmyk { get; set; }
        public double[] Hsl { get; set; }
        public double[] Hsv { get; set; }
        public double[] Xyz { get; set; }
    }
}
