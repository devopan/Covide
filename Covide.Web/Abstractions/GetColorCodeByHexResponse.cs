using Covide.Web.Domain.Models;

namespace Covide.Web.Abstractions
{
    public class GetColorCodeByHexResponse : ConversionModel
    {
        public string HexTriplet { get; set; }
        public string ColorCodeName { get; set; }
    }
}
